using Gateway.v1.Application.Desserializer;
using Gateway.v1.Application.Request;
using Nuget.Client.Input;
using Nuget.Client.Output;
using System.Text.Json;

namespace Gateway.v1.Application.Services.Clients
{
    public class GetClientsService
    {
        private const string BaseURL = "http://localhost:5277/client"; //nome do container
        private const string GetByIdURL = BaseURL + "/ID?ID=";
        public async Task<List<OutputModel>> GetClientsAsync()
        {
            try
            {
                var response = await GetHttpResponseService.GetResponseMessage(BaseURL);
                //Console.WriteLine(response);
                await Task.Delay(3000); 
                var data = await JsonDeserializer.Deserialize<List<OutputModel>>(response);
                return data;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public async Task<OutputModel> GetClientByIdAsync(long ID)
        {
            try
            {

                var response = await GetHttpResponseService.GetResponseMessage(GetByIdURL + ID);
                //Console.WriteLine(response);
                var data = await JsonDeserializer.Deserialize<OutputModel>(response);
                return data;


            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
