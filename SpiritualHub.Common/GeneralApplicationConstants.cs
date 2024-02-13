namespace SpiritualHub.Common;

public static class GeneralApplicationConstants
{
    public const int DefaultPage = 1;
    public const int EntitiesPerPageConstant = 3;

    public const string AdminAreaName = "Admin";
    public const string AdminRoleName = "Administrator";
    public const string AdminEmail = "admin@mail.com";

    public const string OnlineUsersCookieName = "IsOnline";
    public const int LastActivityBeforeOfflineMinutes = 10;

    public const string UserCacheKey = "UsersCache";
    public const int UsersCacheDurationMinutes = 5;

    public const string IFrameHTMLCode = "<iframe width=\"560\" height=\"315\" src=\"{0}\" frameborder=\"0\" allowfullscreen></iframe>";
    public const string EmbedLinkForYouTubeVideo = "https://www.youtube.com/embed/{0}";
    public const string EmbedLinkForVimeoVideo = "https://player.vimeo.com/video/{0}";
}