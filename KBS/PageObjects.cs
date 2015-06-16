using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Collection of constant locators in all the pages of Application Under Test
namespace LCI.QualityTools.BrowserTests.MyLCI
{
    public class PageObjects
    {
        public const string idLoginUserName = "PageContent_Login1_txtUsername";
        public const string idLoginPassword = "PageContent_Login1_txtPassword";
        public const string btnLogin = "PageContent_Login1_btnSubmit";
        public const string mnuLinkDistricts = "a_3_1_28";
        public const string subMenuLinkClubs = "a_3_2_42";
        public const string mnuMyDistrict = "mnuMyDistrict";
        public const string mnuNameMyDistrictClubs = "MyDistrictClubs";
        public const string lblTxtDisplayClubStatus = "lblDiscontinueDeniedStatus";
        public const string lblSearchOption = "lblSearchOptionTitle";
        public const string ulLanguageOptions ="option";
        public const string csmnuLinkDistricts = "a_3_1_30";
        public const string cssubMenuLinkClubs = "a_3_2_35";
        public const string linkTextAddClub = "Add Club";
        public const string idAddClub = "hlAddClub";
        public const string xpathDdlOptions = ".//option";
        public const string xpathListSponsoringClubs = "//div[@class='DistrictClubResults']/div/div/div[1]";
        public const string xpathClubPresidentPanel ="//*[@id='pnlNewClubPresidentHeader']/div/b";
        public const string xpathClubSecretaryPanel = "//*[@id='pnlNewClubSecretaryHeader']/p/b";
        public const string xpathClubConfirmationMsg = "//div[@class='confirmationMessages']/table/tbody/tr/td";
        public const string xpathClubList = "//div[@class='gridHeader']/div/div/div[1]";
        public const string xpathViewAppList = "//a[.='View Application']";
        public const string xpathClubListNextPage = ".//*[@id='myGrid']/div[2]/div[3]/a[2]/img";
        public const string btnSponsoringClub = "btnSelectSponsoringClub";
        public const string idClubName = "txtClubName";
        public const string ddlClubType = "ddlClubType";
        public const string idClubCity = "txtCity";
        public const string ddlclubLang = "ddlClubLanguage";
        public const string sponsoringClub = "btnSelectSponsoringClub";
        public const string idPresidentFirstname = "txtPresidentFirstName";
        public const string idPresidentLastname = "txtPresidentLastName";
        public const string idPresidentYOB = "txtPresidentBirthday";
        public const string idPresidentGender = "ddlPresidentGender";
        public const string idPresidentEmailaddress = "txtPresidentEmailAddress";
        public const string idPresidentClearbtn = "btnClearPresident";
        public const string idSecretaryFirstname = "txtSecretaryFirstName";
        public const string idSecretaryLastname = "txtSecretaryLastName";
        public const string idSecretaryYOB = "txtSecretaryBirthday";
        public const string idSecretaryGender = "ddlSecretaryGender";
        public const string idSsecretaryEmailaddress = "txtSecretaryEmailAddress";
        public const string idsecretaryclearbtn = "btnClearSecretary";
        public const string idNewMemberscount = "txtNewMemberCount";
        public const string idTransferMemberscount = "txtTransferMemberCount";
        public const string idStudentMemberscount = "txtStudentCount";
        public const string idLeoLionscount = "txtLeoLionCount";
        public const string idStudentsOver30Yrs = "txtStudentOver30YrsCount";
        public const string idStudentsUnder30yrs = "txtStudentUnder30YrsCount";
        public const string idYoungAdultscount = "txtYoungAdultMemberCount";
        public const string idClubCriteria = "cbReadNewClubCriteria";
        public const string idComments = "txtNewClubAppComment";
        public const string idBtnSave = "btnSave";
        public const string idBtnCancel= "btnCancel";
        public const string idBtnDelete = "btnDelete";
        public const string idBtnSubmitPendingSubmission = "cbStatusAction_Submit";
        public const string idBtnSubmitPendingDGAuth = "cbStatusAction_DGAuthorize";
        public const string idBtnSubmitPendingLCIAuth = "cbStatusAction_LCIAuthorize";
        public const string idBtnSubmitPendingCompletion = "cbStatusAction_SubmitForFinalApproval";
        public const string idBtnSubmitPendingFinalApproval = "cbStatusAction_LCIApprove";
        public const string xpathErrorLabel = ".//*[@id='lblerr']";
        public const string xpathSchoolName = ".//*[@id='txtSchoolName']";
        public const string xpathClubHelp = ".//*[@id='lblClubNamingHelp']";
        public const string xpathLionessWrng = ".//*[@id='lblLionessWarning']";
        public const string idAddCommentbtn = "btnNewComment";
        public const string idAddModalComments = "txtModalComments";
        public const string btnModalSave = "btnModalCommentsSave";
        public const string xpathLeolionWarning = ".//*[@id='lblLeoLionWarning']";
        public const string xpathTenPlusClubWarning = ".//*[@id='lblDistrictNewClubRequestCountWarning']";
        public const string xpathLabelCaption = ".//*[@id='lblCaption']";
        public const string xpathMsgClubConfirmation = "//div[@id='divNewClubApplication']/div[1]/div/div[2]";
        public const string linkTextMyLCIHome = "Home";
        public const string idNewClubCriteria = "cbReadNewClubCriteria";
        public const string linkTextLogout = "Logout";
        public const string xpathTaskList= "//div[@id='Tab265']/div/ul/li/div/div/a";
        public const string idDiscontinueClubchkbox = "cbStatusAction_Discontinue";
        public const string idDiscontinueNote = "txtDiscontinueNote";
        public const string linkTextGotoApplication = "Go to Application";
        public const string idContinueClub = "cbStatusAction_Continue";
        public const string xpathFindClubQuickViewList = "//*[@id='pnlQuickViews']/li/a";
        public const string xpathClubText = ".//*[@id='form1x']/div[4]/div[1]/div[5]/div[1]/div[1]/div/div[6]/div[2]/div/div";
        public const string xpathFindClubStatusListLabel = " .//*[@id='myGrid']/div[1]";
        public const string xpathAddCommentsNotes = ".//*[@id='notes']";
        public const string cssImgClubHelp = "#imgClubNamingHelp > #Image1";
 

    }
}
