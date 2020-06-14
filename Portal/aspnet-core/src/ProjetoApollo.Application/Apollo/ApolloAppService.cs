using Abp.Configuration;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.MultiTenancy;
using Abp.Net.Mail;
using Abp.ObjectMapping;
using Abp.UI;
using MailKit.Net.Smtp;
using MimeKit;
using ProjetoApollo.Apollo.Core;
using ProjetoApollo.Apollo.Dto;
using ProjetoApollo.Apollo.Dto.Input;
using ProjetoApollo.Apollo.Dto.Output;
using ProjetoApollo.Authorization.Accounts.Dto;
using ProjetoApollo.Authorization.Roles;
using ProjetoApollo.Authorization.Users;
using ProjetoApollo.Configuration;
using ProjetoApollo.Editions;
using ProjetoApollo.MultiTenancy;
using ProjetoApollo.MultiTenancy.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoApollo.Apollo.AppService
{
    public class ApolloAppService : IApolloAppService
    {
        private readonly IEmailSender _emailSender;
        private readonly SettingManager _settingManager;
        private readonly IRepository<Institution, long> _institutionRepository;
        private readonly IRepository<Tenant, int> _tenantRepository;
        private readonly IObjectMapper _objectMapper;
        private readonly TenantManager _tenantManager;
        private readonly EditionManager _editionManager;
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        private readonly IAbpZeroDbMigrator _abpZeroDbMigrator;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly UserRegistrationManager _userRegistrationManager;

        public ApolloAppService(
            IEmailSender emailSender,
            IRepository<Institution, long> institutionRepository,
            IObjectMapper objectMapper,
            IRepository<Tenant, int> tenantRepository,
            TenantManager tenantManager,
            EditionManager editionManager,
            UserManager userManager,
            RoleManager roleManager,
            IUnitOfWorkManager unitOfWorkManager,
            IAbpZeroDbMigrator abpZeroDbMigrator,
            UserRegistrationManager userRegistrationManager,
            SettingManager settingManager)
        {
            _emailSender = emailSender;
            _institutionRepository = institutionRepository;
            _objectMapper = objectMapper;
            _tenantManager = tenantManager;
            _editionManager = editionManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _abpZeroDbMigrator = abpZeroDbMigrator;
            _tenantRepository = tenantRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _userRegistrationManager = userRegistrationManager;
            _settingManager = settingManager;
        }

        public async Task<CreateInstitutionOutput> RegisterInstitution(CreateInstitutionInput input)
        {
            var output = new CreateInstitutionOutput();
            try
            {
                var institution = input.Institution;
                var institutionMapper = _objectMapper.Map<Institution>(institution);
                await _institutionRepository.InsertAsync(institutionMapper);

                var tenancyName = institution.Name.ToLower().Replace(" ", "").Replace(AbpTenantBase.TenancyNameRegex, "");
                var CreateInstitutionTenantInput = new CreateInstitutionTenantInput()
                {
                    TenancyName = tenancyName,
                    InstitutionName = institution.Name,
                    adminEmail = institution.TechnicalContact.Email
                };
                var newTenantUser = await CreateTenant(CreateInstitutionTenantInput);

                #region email content

                var emailHeader = @"<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'><html xmlns='http://www.w3.org/1999/xhtml' xmlns:o='urn:schemas-microsoft-com:office:office' style='width:100%;font-family:arial, 'helvetica neue', helvetica, sans-serif;-webkit-text-size-adjust:100%;-ms-text-size-adjust:100%;padding:0;Margin:0;'><head><meta charset='UTF-8'><meta content='width=device-width, initial-scale=1' name='viewport'><meta name='x-apple-disable-message-reformatting'><meta http-equiv='X-UA-Compatible' content='IE=edge'><meta content='telephone=no' name='format-detection'><title>Cópia de Novo e-mail</title> <!--[if (mso 16)]><style type='text/css'>     a {text-decoration: none;}     </style><![endif]--> <!--[if gte mso 9]><style>sup { font-size: 100% !important; }</style><![endif]--> <!--[if gte mso 9]><xml> <o:OfficeDocumentSettings> <o:AllowPNG></o:AllowPNG> <o:PixelsPerInch>
96</o:PixelsPerInch> </o:OfficeDocumentSettings> </xml><![endif]--><style type='text/css'>
@media only screen and (max-width:600px) {p, ul li, ol li, a { font-size:16px!important; line-height:150%!important } h1 { font-size:30px!important; text-align:center; line-height:120%!important } h2 { font-size:26px!important; text-align:center; line-height:120%!important } h3 { font-size:20px!important; text-align:center; line-height:120%!important } h1 a { font-size:30px!important } h2 a { font-size:26px!important } h3 a { font-size:20px!important } .es-menu td a { font-size:16px!important } .es-header-body p, .es-header-body ul li, .es-header-body ol li, .es-header-body a { font-size:16px!important } .es-footer-body p, .es-footer-body ul li, .es-footer-body ol li, .es-footer-body a { font-size:16px!important } .es-infoblock p, .es-infoblock ul li, .es-infoblock ol li, .es-infoblock a { font-size:12px!important } *[class='gmail-fix'] { display:none!important } .es-m-txt-c, .es-m-txt-c h1, .es-m-txt-c h2, .es-m-txt-c h3 {
text-align:center!important } .es-m-txt-r, .es-m-txt-r h1, .es-m-txt-r h2, .es-m-txt-r h3 { text-align:right!important } .es-m-txt-l, .es-m-txt-l h1, .es-m-txt-l h2, .es-m-txt-l h3 { text-align:left!important } .es-m-txt-r img, .es-m-txt-c img, .es-m-txt-l img { display:inline!important } .es-button-border { display:block!important } a.es-button { font-size:20px!important; display:block!important; border-width:10px 0px 10px 0px!important } .es-btn-fw { border-width:10px 0px!important; text-align:center!important } .es-adaptive table, .es-btn-fw, .es-btn-fw-brdr, .es-left, .es-right { width:100%!important } .es-content table, .es-header table, .es-footer table, .es-content, .es-footer, .es-header { width:100%!important; max-width:600px!important } .es-adapt-td { display:block!important; width:100%!important } .adapt-img { width:100%!important; height:auto!important } .es-m-p0 { padding:0px!important } .es-m-p0r {
padding-right:0px!important } .es-m-p0l { padding-left:0px!important } .es-m-p0t { padding-top:0px!important } .es-m-p0b { padding-bottom:0!important } .es-m-p20b { padding-bottom:20px!important } .es-mobile-hidden, .es-hidden { display:none!important } .es-desk-hidden { display:table-row!important; width:auto!important; overflow:visible!important; float:none!important; max-height:inherit!important; line-height:inherit!important } .es-desk-menu-hidden { display:table-cell!important } table.es-table-not-adapt, .esd-block-html table { width:auto!important } table.es-social { display:inline-block!important } table.es-social td { display:inline-block!important } }#outlook a {	padding:0;}.ExternalClass {	width:100%;}.ExternalClass,.ExternalClass p,.ExternalClass span,.ExternalClass font,.ExternalClass td,.ExternalClass div {	line-height:100%;}.es-button
{	mso-style-priority:100!important;	text-decoration:none!important;}a[x-apple-data-detectors] {	color:inherit!important;	text-decoration:none!important;	font-size:inherit!important;	font-family:inherit!important;	font-weight:inherit!important;	line-height:inherit!important;}.es-desk-hidden {	display:none;	float:left;	overflow:hidden;	width:0;	max-height:0;	line-height:0;	mso-hide:all;}</style></head><body style='width:100%;font-family:arial, 'helvetica neue', helvetica, sans-serif;-webkit-text-size-adjust:100%;-ms-text-size-adjust:100%;padding:0;Margin:0;'><div class='es-wrapper-color' style='background-color:#F6F6F6;'> <!--[if gte mso 9]><v:background xmlns:v='urn:schemas-microsoft-com:vml' fill='t'> <v:fill type='tile' color='#f6f6f6'></v:fill> </v:background><![endif]-->";

                var emailBody = $@"<table class='es-wrapper' width='100%' cellspacing='0' cellpadding='0' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;padding:0;Margin:0;width:100%;height:100%;background-repeat:repeat;background-position:center top;'><tr style='border-collapse:collapse;'><td valign='top' style='padding:0;Margin:0;'><table class='es-content' cellspacing='0' cellpadding='0' align='center' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%;'><tr style='border-collapse:collapse;'><td align='center' style='padding:0;Margin:0;'><table class='es-content-body' width='600' cellspacing='0' cellpadding='0' bgcolor='#ffffff' align='center' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#FFFFFF;'><tr style='border-collapse:collapse;'>
<td align='left' style='padding:0;Margin:0;padding-top:20px;padding-left:20px;padding-right:20px;'><table width='100%' cellspacing='0' cellpadding='0' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;'><tr style='border-collapse:collapse;'><td width='560' valign='top' align='center' style='padding:0;Margin:0;'><table width='100%' cellspacing='0' cellpadding='0' role='presentation' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;'><tr style='border-collapse:collapse;'><td align='center' bgcolor='#89BEEF' style='padding:0;Margin:0;'><p style='Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-size:26px;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:39px;color:#060606;'>Bem vindo ao&nbsp;<strong>Projeto Apollo</strong>!!<span style='background-color:#0000CD;'></span></p></td></tr>
</table></td></tr></table></td></tr><tr style='border-collapse:collapse;'><td align='left' style='padding:0;Margin:0;padding-top:20px;padding-left:20px;padding-right:20px;'><table cellpadding='0' cellspacing='0' width='100%' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;'><tr style='border-collapse:collapse;'><td width='560' align='center' valign='top' style='padding:0;Margin:0;'><table cellpadding='0' cellspacing='0' width='100%' role='presentation' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;'><tr style='border-collapse:collapse;'><td align='left' style='padding:0;Margin:0;'><p style='Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-size:14px;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;color:#333333;'>Bem vindo {institution.Name},<br><br>
O acesso de sua instituição&nbsp;ao <strong>Projeto Apollo</strong>&nbsp;foi criado com sucesso.<br><br>Seu nome de usuário é: <font color='#bc93c5'><b>Admin</b></font><br>Sua senha foi gerada automaticamente: <span style='color:#C29B55;'><strong>{newTenantUser.Password}<br></strong></span><span style='color:null;'><strong><span style='font-size:12px;'>É possivel alterar a senha posteriormente</span></strong></span><span style='color:#C29B55;'><strong></strong></span><br></p></td></tr></table></td></tr></table></td></tr><tr style='border-collapse:collapse;'><td align='left' style='padding:0;Margin:0;padding-top:20px;padding-left:20px;padding-right:20px;'> <!--[if mso]><table width='560' cellpadding='0' cellspacing='0'><tr><td width='194' valign='top'><![endif]--><table cellpadding='0' cellspacing='0' class='es-left' align='left' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left;'>
<tr style='border-collapse:collapse;'><td width='174' class='es-m-p0r es-m-p20b' align='center' style='padding:0;Margin:0;'><table cellpadding='0' cellspacing='0' width='100%' role='presentation' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;'><tr style='border-collapse:collapse;'><td align='center' style='padding:0;Margin:0;'><p style='Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-size:14px;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;color:#333333;'><strong>Acesso Gerencial</strong></p></td></tr></table></td><td class='es-hidden' width='20' style='padding:0;Margin:0;'></td></tr></table> <!--[if mso]></td><td width='173' valign='top'><![endif]-->
<table cellpadding='0' cellspacing='0' class='es-left' align='left' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left;'><tr style='border-collapse:collapse;'><td width='173' class='es-m-p20b' align='center' style='padding:0;Margin:0;'><table cellpadding='0' cellspacing='0' width='100%' role='presentation' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;'><tr style='border-collapse:collapse;'><td align='center' style='padding:0;Margin:0;'><p style='Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-size:14px;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;color:#333333;'><strong>Acesso Medico</strong></p></td></tr></table></td></tr></table> <!--[if mso]></td><td width='20'></td><td width='173' valign='top'><![endif]-->
<table cellpadding='0' cellspacing='0' class='es-right' align='right' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:right;'><tr style='border-collapse:collapse;'><td width='173' align='center' style='padding:0;Margin:0;'><table cellpadding='0' cellspacing='0' width='100%' role='presentation' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;'><tr style='border-collapse:collapse;'><td align='center' style='padding:0;Margin:0;'><p style='Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-size:14px;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;color:#333333;'><strong>Acesso Lobby</strong></p></td></tr></table></td></tr></table> <!--[if mso]></td></tr></table><![endif]--></td></tr><tr style='border-collapse:collapse;'>
<td align='left' style='padding:0;Margin:0;padding-top:20px;padding-left:20px;padding-right:20px;'> <!--[if mso]><table width='560' cellpadding='0' cellspacing='0'><tr><td width='194' valign='top'><![endif]--><table cellpadding='0' cellspacing='0' class='es-left' align='left' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left;'><tr style='border-collapse:collapse;'><td width='174' class='es-m-p0r es-m-p20b' align='center' style='padding:0;Margin:0;'><table cellpadding='0' cellspacing='0' width='100%' role='presentation' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;'><tr style='border-collapse:collapse;'><td align='left' style='padding:0;Margin:0;'>
<p style='Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-size:14px;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;color:#333333;'>Com o <strong>Acesso Gerencia</strong>l é possível:</p><ul><li style='-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-size:14px;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;Margin-bottom:15px;color:#333333;'>Criar novos acessos</li><li style='-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-size:14px;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;Margin-bottom:15px;color:#333333;'>Verificar Logs de erro</li>
<li style='-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-size:14px;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;Margin-bottom:15px;color:#333333;'>Acesso a gráficos em <em>real time</em></li><li style='-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-size:14px;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;Margin-bottom:15px;color:#333333;'>Verificar consultas realizadas</li></ul></td></tr></table></td><td class='es-hidden' width='20' style='padding:0;Margin:0;'></td></tr></table> <!--[if mso]></td><td width='173' valign='top'><![endif]--><table cellpadding='0' cellspacing='0' class='es-left' align='left' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left;'><tr style='border-collapse:collapse;'>
<td width='173' class='es-m-p20b' align='center' style='padding:0;Margin:0;'><table cellpadding='0' cellspacing='0' width='100%' role='presentation' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;'><tr style='border-collapse:collapse;'><td align='left' style='padding:0;Margin:0;'><p style='Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-size:14px;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;color:#333333;'>Com o<strong> Acesso Médico</strong> é possivel:</p><ul><li style='-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-size:14px;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;Margin-bottom:15px;color:#333333;'>Consultar histórico de consultas realizadas</li>
<li style='-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-size:14px;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;Margin-bottom:15px;color:#333333;'>Verificar histórico do paciente</li></ul></td></tr></table></td></tr></table> <!--[if mso]></td><td width='20'></td><td width='173' valign='top'><![endif]--><table cellpadding='0' cellspacing='0' class='es-right' align='right' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:right;'><tr style='border-collapse:collapse;'><td width='173' align='center' style='padding:0;Margin:0;'><table cellpadding='0' cellspacing='0' width='100%' role='presentation' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;'><tr style='border-collapse:collapse;'><td align='left' style='padding:0;Margin:0;'>
<p style='Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-size:14px;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;color:#333333;'>Com o <strong>Acesso Lobby</strong> é possível:<br></p><ul><li style='-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-size:14px;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;Margin-bottom:15px;color:#333333;'>Receber notificação de novos pacientes com dados para cadastro e previsão de chegada do mesmo</li></ul></td></tr></table></td></tr></table> <!--[if mso]></td></tr></table><![endif]--></td></tr></table></td></tr></table></td></tr></table></div></body>
</html>

";
                var emailContent = emailHeader + emailBody;

                #endregion email content

                await SendEmail(institution.Name, institution.TechnicalContact.Email, "(NoReply) Cadastro realizado com sucesso", emailContent);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return output;
        }

        public InstitutionDto CreateInstitution()
        {
            var output = new InstitutionDto();
            var emptyAddress = new AddressDto()
            {
                CEP = string.Empty,
                AddressLine = string.Empty,
                Number = string.Empty,
                Complement = string.Empty,
                District = string.Empty,
                City = string.Empty,
                State = string.Empty
            };
            var emptyListAddress = new List<AddressDto>();
            emptyListAddress.Add(emptyAddress);
            output.Addresses = emptyListAddress;
            output.TechnicalContact = new ContactDto();
            output.BillingInfo = new BillingDto();
            return output;
        }

        public async Task<RegisterOutput> RegisterClient(RegisterInput input)
        {
            var user = new User();
            try
            {
                user = await _userRegistrationManager.RegisterClientAsync(
                    input.Name,
                    input.Surname,
                    input.EmailAddress,
                    input.UserName,
                    input.Password,
                    true // Assumed email address is always confirmed. Change this if you want to implement email confirmation.
                );

                #region email content

                var emailHeader = @"<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'><html xmlns='http://www.w3.org/1999/xhtml' xmlns:o='urn:schemas-microsoft-com:office:office' style='width:100%;font-family:arial, 'helvetica neue', helvetica, sans-serif;-webkit-text-size-adjust:100%;-ms-text-size-adjust:100%;padding:0;Margin:0;'><head><meta charset='UTF-8'><meta content='width=device-width, initial-scale=1' name='viewport'><meta name='x-apple-disable-message-reformatting'><meta http-equiv='X-UA-Compatible' content='IE=edge'><meta content='telephone=no' name='format-detection'><title>Novo e-mail</title> <!--[if (mso 16)]><style type='text/css'>     a {text-decoration: none;}     </style><![endif]--> <!--[if gte mso 9]><style>sup { font-size: 100% !important; }</style><![endif]--> <!--[if gte mso 9]><xml> <o:OfficeDocumentSettings> <o:AllowPNG></o:AllowPNG> <o:PixelsPerInch>96</o:PixelsPerInch>
 </o:OfficeDocumentSettings> </xml><![endif]--><style type='text/css'>
@media only screen and (max-width:600px) {p, ul li, ol li, a { font-size:16px!important; line-height:150%!important } h1 { font-size:30px!important; text-align:center; line-height:120%!important } h2 { font-size:26px!important; text-align:center; line-height:120%!important } h3 { font-size:20px!important; text-align:center; line-height:120%!important } h1 a { font-size:30px!important } h2 a { font-size:26px!important } h3 a { font-size:20px!important } .es-menu td a { font-size:16px!important } .es-header-body p, .es-header-body ul li, .es-header-body ol li, .es-header-body a { font-size:16px!important } .es-footer-body p, .es-footer-body ul li, .es-footer-body ol li, .es-footer-body a { font-size:16px!important } .es-infoblock p, .es-infoblock ul li, .es-infoblock ol li, .es-infoblock a { font-size:12px!important } *[class='gmail-fix'] { display:none!important } .es-m-txt-c, .es-m-txt-c h1, .es-m-txt-c h2, .es-m-txt-c h3 {
text-align:center!important } .es-m-txt-r, .es-m-txt-r h1, .es-m-txt-r h2, .es-m-txt-r h3 { text-align:right!important } .es-m-txt-l, .es-m-txt-l h1, .es-m-txt-l h2, .es-m-txt-l h3 { text-align:left!important } .es-m-txt-r img, .es-m-txt-c img, .es-m-txt-l img { display:inline!important } .es-button-border { display:block!important } a.es-button { font-size:20px!important; display:block!important; border-width:10px 0px 10px 0px!important } .es-btn-fw { border-width:10px 0px!important; text-align:center!important } .es-adaptive table, .es-btn-fw, .es-btn-fw-brdr, .es-left, .es-right { width:100%!important } .es-content table, .es-header table, .es-footer table, .es-content, .es-footer, .es-header { width:100%!important; max-width:600px!important } .es-adapt-td { display:block!important; width:100%!important } .adapt-img { width:100%!important; height:auto!important } .es-m-p0 { padding:0px!important } .es-m-p0r {
padding-right:0px!important } .es-m-p0l { padding-left:0px!important } .es-m-p0t { padding-top:0px!important } .es-m-p0b { padding-bottom:0!important } .es-m-p20b { padding-bottom:20px!important } .es-mobile-hidden, .es-hidden { display:none!important } .es-desk-hidden { display:table-row!important; width:auto!important; overflow:visible!important; float:none!important; max-height:inherit!important; line-height:inherit!important } .es-desk-menu-hidden { display:table-cell!important } table.es-table-not-adapt, .esd-block-html table { width:auto!important } table.es-social { display:inline-block!important } table.es-social td { display:inline-block!important } }#outlook a {	padding:0;}.ExternalClass {	width:100%;}.ExternalClass,.ExternalClass p,.ExternalClass span,.ExternalClass font,.ExternalClass td,.ExternalClass div {	line-height:100%;}.es-button
{	mso-style-priority:100!important;	text-decoration:none!important;}a[x-apple-data-detectors] {	color:inherit!important;	text-decoration:none!important;	font-size:inherit!important;	font-family:inherit!important;	font-weight:inherit!important;	line-height:inherit!important;}.es-desk-hidden {	display:none;	float:left;	overflow:hidden;	width:0;	max-height:0;	line-height:0;	mso-hide:all;}</style></head><body style='width:100%;font-family:arial, 'helvetica neue', helvetica, sans-serif;-webkit-text-size-adjust:100%;-ms-text-size-adjust:100%;padding:0;Margin:0;'><div class='es-wrapper-color' style='background-color:#F6F6F6;'> <!--[if gte mso 9]><v:background xmlns:v='urn:schemas-microsoft-com:vml' fill='t'> <v:fill type='tile' color='#f6f6f6'></v:fill> </v:background><![endif]-->";

                var emailBody = $@"<table class='es-wrapper' width='100%' cellspacing='0' cellpadding='0' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;padding:0;Margin:0;width:100%;height:100%;background-repeat:repeat;background-position:center top;'><tr style='border-collapse:collapse;'><td valign='top' style='padding:0;Margin:0;'><table class='es-content' cellspacing='0' cellpadding='0' align='center' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%;'><tr style='border-collapse:collapse;'><td align='center' style='padding:0;Margin:0;'><table class='es-content-body' width='600' cellspacing='0' cellpadding='0' bgcolor='#ffffff' align='center' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#FFFFFF;'><tr style='border-collapse:collapse;'>
<td align='left' style='padding:0;Margin:0;padding-top:20px;padding-left:20px;padding-right:20px;'><table width='100%' cellspacing='0' cellpadding='0' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;'><tr style='border-collapse:collapse;'><td width='560' valign='top' align='center' style='padding:0;Margin:0;'><table width='100%' cellspacing='0' cellpadding='0' role='presentation' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;'><tr style='border-collapse:collapse;'><td align='center' bgcolor='#89BEEF' style='padding:0;Margin:0;'><p style='Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-size:26px;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:39px;color:#060606;'>Bem vindo ao&nbsp;<strong>Projeto Apollo</strong>!!<span style='background-color:#0000CD;'></span></p></td></tr>
</table></td></tr></table></td></tr><tr style='border-collapse:collapse;'><td align='left' style='padding:0;Margin:0;padding-top:20px;padding-left:20px;padding-right:20px;'><table cellpadding='0' cellspacing='0' width='100%' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;'><tr style='border-collapse:collapse;'><td width='560' align='center' valign='top' style='padding:0;Margin:0;'><table cellpadding='0' cellspacing='0' width='100%' role='presentation' style='mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;'><tr style='border-collapse:collapse;'><td align='left' style='padding:0;Margin:0;'><p style='Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-size:14px;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;color:#333333;'>Bem vindo {user.FullName},<br><br>Seu acesso ao&nbsp;<strong>
Projeto Apollo</strong>&nbsp;foi criado com sucesso.<br><br>Com ele é possivel marcar consultas, acessar historico e muito mais<br><br>Seu nome de usuario é: <span style='color:#BC93C5;'><strong>{user.UserName}</strong></span></p></td></tr></table></td></tr></table></td></tr></table></td></tr></table></td></tr></table></div></body>
</html>";
                var emailContent = emailHeader + emailBody;

                #endregion email content

                await SendEmail(user.FullName, user.EmailAddress, "(NoReply) Cadastro realizado com sucesso", emailContent);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return new RegisterOutput
            {
                CanLogin = user.IsActive && true
            };
        }

        private async Task SendEmail(string toName, string to, string subject, string body)
        {
            var emailUsername = _settingManager.GetSettingValueForApplication(AppSettingNames.EmailUsername);
            var emailPassword = _settingManager.GetSettingValueForApplication(AppSettingNames.EmailPassword);
            var emailUseSSL = bool.Parse(_settingManager.GetSettingValueForApplication(AppSettingNames.EmailUseSSL));
            var emailSMTPServer = _settingManager.GetSettingValueForApplication(AppSettingNames.EmailSMTPServer);
            var emailSMTPPort = _settingManager.GetSettingValueForApplication(AppSettingNames.EmailSMTPPort);
            var mailMessage = new MimeMessage();

            mailMessage.From.Add(new MailboxAddress("Projeto Apollo", emailUsername));
            mailMessage.To.Add(new MailboxAddress(toName, to));
            mailMessage.Subject = subject;
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = body;
            mailMessage.Body = bodyBuilder.ToMessageBody();
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.Connect(emailSMTPServer, int.Parse(emailSMTPPort), emailUseSSL);
                smtpClient.Authenticate(emailUsername, emailPassword);
                smtpClient.Send(mailMessage);
                smtpClient.Disconnect(true);
            }
        }

        private async Task<User> CreateTenant(CreateInstitutionTenantInput input)
        {
            var tenantDto = new CreateTenantDto()
            {
                TenancyName = input.TenancyName,
                Name = input.InstitutionName,
                AdminEmailAddress = input.adminEmail,
                IsActive = true
            };
            if (_tenantRepository.GetAll().Where(t => t.TenancyName == input.TenancyName).ToList().Count() > 0)
            {
                throw new UserFriendlyException("Ja existe uma instituição com este nome");
            }

            var CurrentUnitOfWork = _unitOfWorkManager.Current;
            var tenantService = new TenantAppService(_tenantRepository, _tenantManager, _editionManager, _userManager, _roleManager, _abpZeroDbMigrator);

            var adminUser = new User();
            try
            {
                var tenant = _objectMapper.Map<Tenant>(tenantDto);
                tenant.ConnectionString = null;

                var defaultEdition = await _editionManager.FindByNameAsync(EditionManager.DefaultEditionName);
                if (defaultEdition != null)
                {
                    tenant.EditionId = defaultEdition.Id;
                }

                await _tenantManager.CreateAsync(tenant);
                await CurrentUnitOfWork.SaveChangesAsync(); // To get new tenant's id.

                // Create tenant database
                _abpZeroDbMigrator.CreateOrMigrateForTenant(tenant);

                // We are working entities of new tenant, so changing tenant filter
                using (CurrentUnitOfWork.SetTenantId(tenant.Id))
                {
                    // Create static roles for new tenant
                    tenantService.CheckErrors(await _roleManager.CreateStaticRoles(tenant.Id));

                    await CurrentUnitOfWork.SaveChangesAsync(); // To get static role ids

                    // Grant all permissions to admin role
                    var adminRole = _roleManager.Roles.Single(r => r.Name == StaticRoleNames.Tenants.Admin);
                    await _roleManager.GrantAllPermissionsAsync(adminRole);

                    // Create admin user for the tenant
                    adminUser = User.CreateTenantAdminUser(tenant.Id, tenantDto.AdminEmailAddress);
                    await _userManager.InitializeOptionsAsync(tenant.Id);
                    string password = User.CreateRandomPassword();
                    tenantService.CheckErrors(await _userManager.CreateAsync(adminUser, password));
                    await CurrentUnitOfWork.SaveChangesAsync(); // To get admin user's id

                    // Assign admin user to role!
                    tenantService.CheckErrors(await _userManager.AddToRoleAsync(adminUser, adminRole.Name));
                    await CurrentUnitOfWork.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return adminUser;
        }
    }
}