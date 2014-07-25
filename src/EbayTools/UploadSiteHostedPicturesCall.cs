using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using eBay.Service.Core.Sdk;

namespace EbayTools
{
    public class UploadSiteHostedPicturesCall
    {
        public ApiContext ApiContext { get; set; }

        public UploadSiteHostedPicturesCall(ApiContext apiContext)
        {
            ApiContext = apiContext;
        }

        public string Token
        {
            get { return ApiContext.ApiCredential.eBayToken; }
        }

        public string DevID
        {
            get { return ApiContext.ApiCredential.ApiAccount.Developer; }
        }
        
        public string AppID
        {
            get { return ApiContext.ApiCredential.ApiAccount.Application; }
        }

        public string CertID
        {
            get { return ApiContext.ApiCredential.ApiAccount.Certificate; }
        }

        public string ApiURL
        {
            get { return ApiContext.XmlApiServerUrl ?? "https://api.ebay.com/ws/api.dll"; }
        }

        public string Version
        {
            get { return ApiContext.Version; }
        }

        public string SiteID
        {
            get { return ((int) ApiContext.Site).ToString(CultureInfo.InvariantCulture); }
        }

        public string CallName
        {
            get { return "UploadSiteHostedPictures"; }
        }

        public string RequestXML
        {
            get
            {
                return "<?xml version=\"1.0\" encoding=\"utf-8\"?><UploadSiteHostedPicturesRequest xmlns=\"urn:ebay:apis:eBLBaseComponents\"><ExternalPictureURL>{0}</ExternalPictureURL><RequesterCredentials><eBayAuthToken>{1}</eBayAuthToken></RequesterCredentials></UploadSiteHostedPicturesRequest>";
            }
        }

        public string Execute(string externalURL)
        {
            var req = WebRequest.CreateHttp(ApiURL);

            req.Headers.Add("X-EBAY-API-COMPATIBILITY-LEVEL", Version);
            req.Headers.Add("X-EBAY-API-SITEID", SiteID);
            req.Headers.Add("X-EBAY-API-CALL-NAME", CallName);
            req.Headers.Add("X-EBAY-API-DEV-NAME", DevID);
            req.Headers.Add("X-EBAY-API-APP-NAME", AppID);
            req.Headers.Add("X-EBAY-API-CERT-NAME", CertID);

            req.Method = "POST";

            var bytes = Encoding.ASCII.GetBytes(string.Format(RequestXML, externalURL, Token));
            req.ContentLength = bytes.Length;

            using (var stream = req.GetRequestStream())
                stream.Write(bytes, 0, bytes.Length);

            try
            {
                var response = req.GetResponse();
                using (var reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    var doc = new XmlDocument();
                    doc.Load(reader);

                    var nsm = new XmlNamespaceManager(doc.NameTable);
                    nsm.AddNamespace("ns", "urn:ebay:apis:eBLBaseComponents");

                    var urlNode = doc.SelectSingleNode("//ns:FullURL", nsm);
                    return urlNode.InnerText;
                }
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, ex);
            }
        }
    }
}