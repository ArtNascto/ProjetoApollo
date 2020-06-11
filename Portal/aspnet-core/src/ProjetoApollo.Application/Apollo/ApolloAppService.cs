using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.MultiTenancy;
using Abp.ObjectMapping;
using Microsoft.AspNetCore.Identity;
using ProjetoApollo.Apollo.Core;
using ProjetoApollo.Apollo.Dto;
using ProjetoApollo.Apollo.Dto.Input;
using ProjetoApollo.Apollo.Dto.Output;
using ProjetoApollo.Authorization.Roles;
using ProjetoApollo.Authorization.Users;
using ProjetoApollo.Editions;
using ProjetoApollo.MultiTenancy;
using ProjetoApollo.MultiTenancy.Dto;

namespace ProjetoApollo.Apollo.AppService {
    public class ApolloAppService : IApolloAppService {
        private readonly IRepository<Address, long> _addressRepository;
        private readonly IRepository<Billing, int> _bilingRepository;
        private readonly IRepository<Contact, int> _contactRepository;
        private readonly IRepository<Institution, long> _institutionRepository;
        private readonly IRepository<Tenant, int> _tenantRepository;
        private readonly IObjectMapper _objectMapper;
        private readonly TenantManager _tenantManager;
        private readonly EditionManager _editionManager;
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        private readonly IAbpZeroDbMigrator _abpZeroDbMigrator;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        public ApolloAppService (
            IRepository<Address, long> addressRepository,
            IRepository<Billing, int> bilingRepository,
            IRepository<Contact, int> contactRepository,
            IRepository<Institution, long> institutionRepository,
            IObjectMapper objectMapper,
            IRepository<Tenant, int> tenantRepository,
            TenantManager tenantManager,
            EditionManager editionManager,
            UserManager userManager,
            RoleManager roleManager,
            IUnitOfWorkManager unitOfWorkManager,
            IAbpZeroDbMigrator abpZeroDbMigrator) {
            _addressRepository = addressRepository;
            _bilingRepository = bilingRepository;
            _contactRepository = contactRepository;
            _institutionRepository = institutionRepository;
            _objectMapper = objectMapper;
            _tenantManager = tenantManager;
            _editionManager = editionManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _abpZeroDbMigrator = abpZeroDbMigrator;
            _tenantRepository = tenantRepository;
            _unitOfWorkManager = unitOfWorkManager;
        }
        public async Task<CreateInstitutionOutput> RegisterInstitution (CreateInstitutionInput input) {
            var output = new CreateInstitutionOutput ();
            try {
                var institution = input.Institution;
                var institutionMapper = _objectMapper.Map<Institution> (institution);
                await _institutionRepository.InsertAsync (institutionMapper);

                var tenancyName = institution.Name.ToLower ().Replace (" ", "").Replace (AbpTenantBase.TenancyNameRegex, "");
                var CreateInstitutionTenantInput = new CreateInstitutionTenantInput () {
                    TenancyName = tenancyName,
                    InstitutionName = institution.Name,
                    adminEmail = institution.TechnicalContact.Email
                };
                var newTenant = CreateTenant (CreateInstitutionTenantInput);
            } catch (Exception e) {
                Console.WriteLine (e);
            }
            return output;
        }
        public InstitutionDto CreateInstitution () {
            var output = new InstitutionDto ();
            var emptyAddress = new AddressDto () {
                CEP = string.Empty,
                AddressLine = string.Empty,
                Number = string.Empty,
                Complement = string.Empty,
                District = string.Empty,
                City = string.Empty,
                State = string.Empty
            };
            var emptyListAddress = new List<AddressDto> ();
            emptyListAddress.Add (emptyAddress);
            output.Addresses = emptyListAddress;
            output.TechnicalContact = new ContactDto ();
            output.BillingInfo = new BillingDto ();
            return output;
        }
        private async Task<bool> CreateTenant (CreateInstitutionTenantInput input) {
            var tenantDto = new CreateTenantDto () {
                TenancyName = input.TenancyName,
                Name = input.InstitutionName,
                AdminEmailAddress = input.adminEmail,
                IsActive = true
            };
            // using (var unitOfWork = _unitOfWorkManager.Begin ()) {
            var CurrentUnitOfWork = _unitOfWorkManager.Current;
            var tenantService = new TenantAppService (_tenantRepository, _tenantManager, _editionManager, _userManager, _roleManager, _abpZeroDbMigrator);
            //     // var newTenant = await tenantService.CreateInstitutionTenantAsync (tenantDto);

            // }

            try {
                var tenant = _objectMapper.Map<Tenant> (tenantDto);
                tenant.ConnectionString = null;

                var defaultEdition = await _editionManager.FindByNameAsync (EditionManager.DefaultEditionName);
                if (defaultEdition != null) {
                    tenant.EditionId = defaultEdition.Id;
                }

                await _tenantManager.CreateAsync (tenant);
                await CurrentUnitOfWork.SaveChangesAsync (); // To get new tenant's id.

                // Create tenant database
                _abpZeroDbMigrator.CreateOrMigrateForTenant (tenant);

                // We are working entities of new tenant, so changing tenant filter
                using (CurrentUnitOfWork.SetTenantId (tenant.Id)) {
                    // Create static roles for new tenant
                    tenantService.CheckErrors (await _roleManager.CreateStaticRoles (tenant.Id));

                    await CurrentUnitOfWork.SaveChangesAsync (); // To get static role ids

                    // Grant all permissions to admin role
                    var adminRole = _roleManager.Roles.Single (r => r.Name == StaticRoleNames.Tenants.Admin);
                    await _roleManager.GrantAllPermissionsAsync (adminRole);

                    // Create admin user for the tenant
                    var adminUser = User.CreateTenantAdminUser (tenant.Id, tenantDto.AdminEmailAddress);
                    await _userManager.InitializeOptionsAsync (tenant.Id);
                    tenantService.CheckErrors (await _userManager.CreateAsync (adminUser, User.DefaultPassword));
                    await CurrentUnitOfWork.SaveChangesAsync (); // To get admin user's id

                    // Assign admin user to role!
                    tenantService.CheckErrors (await _userManager.AddToRoleAsync (adminUser, adminRole.Name));
                    await CurrentUnitOfWork.SaveChangesAsync ();
                }
            } catch (Exception e) {
                Console.WriteLine (e);
            }
            return true;
        }

    }
}