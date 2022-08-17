namespace WebServer.Modules.HostingInformation
{
    public interface IServerInformationProvider
    {
        public Uri BaseURI { get; set; }
    }
}
