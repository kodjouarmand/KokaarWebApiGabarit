namespace Catsa.Domain.RequestFeatures
{
    class ProxyParameters : RequestParameters
    {
        public ProxyParameters() { OrderBy = "name"; }       
        public string SearchTerm { get; set; }
    }
}
