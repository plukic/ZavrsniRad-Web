/// <summary>
/// Constants for application routes
/// </summary>
namespace COAssistance.WEB.Constants
{
    #region Home Routes

    public static class HomeRoutes
    {
        public const string Index = "";

        public const string Data = "Home.Data";
    }

    #endregion Home Routes

    #region GasStation Routes

    public static class GasStationRoutes
    {
        public const string Index = "GasStation.Index";

        public const string Create = "GasStation.Create";

        public const string Edit = "GasStation.Edit";

        public const string Data = "GasStation.Data";
    }

    #endregion GasStation Routes

    #region GasCompany Routes

    public static class GasCompanyRoutes
    {
        public const string Data = "GasCompany.Data";

        public const string Create = "GasCompany.Create";

        public const string CancelCreation = "GasCompany.CancelCreation";

        public const string CreateLogo = "GasCompany.CreateLogo";

        public const string EditLogo = "GasCompany.EditLogo";

        public const string Edit = "GasCompany.Edit";

        public const string Details = "GasCompany.Details";
    }

    #endregion GasCompany Routes

    #region Higway Routes

    public static class HighwayRoutes
    {
        public const string Index = "Highway.Index";

        public const string Create = "Highway.Create";

        public const string Edit = "Highway.Edit";

        public const string Data = "Highway.Data";

        public const string Delete = "Highway.Delete";

        public const string Restore = "Highway.Restore";
    }

    #endregion Higway Routes

    #region Cameras Routes

    public static class CamerasRoutes
    {
        public const string Index = "Cameras.Index";

        public const string Create = "Cameras.Create";

        public const string Edit = "Cameras.Edit";

        public const string Data = "Cameras.Data";

        public const string Details = "Cameras.Details";
    }

    #endregion Cameras Routes

    #region Tollbooth Routes

    public static class TollboothRoutes
    {
        public const string Create = "Tollbooth.Create";

        public const string Edit = "Tollbooth.Edit";

        public const string Data = "Tollbooth.Data";

        public const string Delete = "Tollbooth.Delete";

        public const string Restore = "Tollbooth.Restore";
    }

    #endregion Tollbooth Routes

    #region HighwayRoute Routes

    public static class HighwayRouteRoutes
    {
        public const string Create = "HighwayRoute.Create";

        public const string Edit = "HighwayRoute.Edit";

        public const string Data = "HighwayRoute.Data";

        public const string Delete = "HighwayRoute.Delete";

        public const string Restore = "HighwayRoute.Restore";

        public const string Prices = "HighwayRoute.Prices";
    }

    #endregion HighwayRoute Routes

    #region Staff Routes

    public static class StaffRoutes
    {
        public const string Index = "Staff.Index";

        public const string Data = "Staff.Data";

        public const string Create = "Staff.Create";

        public const string ResetPassword = "Staff.ResetPassword";

        public const string ChangeStatus = "Staff.ChangeStatus";
    }

    #endregion Staff Routes

    #region Notification Routes

    public static class NotificationRoutes
    {
        public const string Data = "Notification.Data";
    }

    #endregion Notification Routes

    #region Profile Routes

    public static class ProfileRoutes
    {
        public const string Index = "Profile.Index";

        public const string Data = "Profile.Data";

        public const string ChangePassword = "Staff.ChangePassword";
    }

    #endregion Profile Routes

    #region Common Routes

    public static class CommonRoutes
    {
        public const string Confirmation = "Common.Confirmation";
    }

    #endregion Common Routes

    #region UserGroups Routes

    public static class UserGroupsRoutes
    {
        public const string Index = "UserGroups.Index";

        public const string Data = "UserGroups.Data";

        public const string Edit = "UserGroups.Edit";
    }

    #endregion UserGroups Routes

    #region HelpRequests Routes

    public static class HelpRequestsRoutes
    {
        public const string Index = "HelpRequests.Index";

        public const string Data = "HelpRequests.Data";

        public const string Details = "HelpRequests.Details";

        public const string Complete = "HelpRequests.Complete";

        public const string Responses = "HelpRequests.Responses";

        public const string ResponseDetails = "HelpRequests.ResponseDetails";

        public const string ResponseActivation = "HelpRequests.ResponseActivation";
    }

    #endregion HelpRequests Routes

    #region RoadCondition Routes

    public static class RoadConditionRoutes
    {
        public const string Index = "RoadCondition.Index";

        public const string Data = "RoadCondition.Data";

        public const string Details = "RoadCondition.Details";

        public const string Create = "RoadCondition.Create";

        public const string Edit = "RoadCondition.Edit";

        public const string Delete = "RoadCondition.Delete";

        public const string Restore = "RoadCondition.Restore";
    }

    #endregion RoadCondition Routes

    #region Client Routes

    public static class ClientRoutes
    {
        public const string Index = "Client.Index";

        public const string Data = "Client.Data";

        public const string Create = "Client.Create";

        public const string Details = "Client.Details";

        public const string Edit = "Client.Edit";

        public const string ChangeStatus = "Client.ChangeStatus";

        public const string ResetPassword = "Client.ResetPassword";

        public const string EmergencyNumbers = "Client.EmergencyNumbers";
    }

    #endregion Client Routes

    #region InsurancePolicy Routes

    public static class InsurancePolicyRoutes
    {
        public const string ClientInsurances = "InsurancePolicy.ClientInsurances";

        public const string Create = "InsurancePolicy.Create";

        public const string Edit = "InsurancePolicy.Edit";
    }

    #endregion InsurancePolicy Routes

    #region Location Routes

    public static class LocationRoutes
    {
        public const string Index = "Location.Index";

        public const string Data = "Location.Data";
    }

    #endregion Location Routes

    #region Access Routes

    public static class AccessRoutes
    {
        public const string SignIn = "Access.SignIn";

        public const string SignOut = "Access.SignOut";
    }

    #endregion Access Routes

    #region Maintenance Routes

    public static class MaintenanceRoutes
    {
        public const string Index = "Maintenance.Index";

        public const string Data = "Maintenance.Data";

        public const string Edit = "Maintenance.Edit";
    }

    #endregion Maintenance Routes

    #region ClientLoginData Routes

    public static class ClientLoginDataRoutes
    {
        public const string Deactivate = "ClientLoginData.Deactivate";

        public const string Data = "ClientLoginData.Data";
    }

    #endregion ClientLoginData Routes
}