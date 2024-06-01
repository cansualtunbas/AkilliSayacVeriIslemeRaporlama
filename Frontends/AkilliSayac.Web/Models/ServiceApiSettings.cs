namespace AkilliSayac.Web.Models
{
    public class ServiceApiSettings
    {
        public string IdentityBaseUri { get; set; }
        public string GatewayBaseUri { get; set; }

        public ServiceApi Counter { get; set; }

        public ServiceApi Report { get; set; }

    }
    public class ServiceApi
    {
        public string Path { get; set; }
    }
}
