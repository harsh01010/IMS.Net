namespace IMS.Web.Services.IServices
{
    public interface ITokenProvider
    {
        void SetToken(string token);
        string? GetToken();

        public string? GetId();
        public void SetId(string id);

		void ClearToken();


    }
}
