using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCM_Notice.Views;
using FCM_Notice.Repositories;
using FCM_Notice.Modules;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;

namespace FCM_Notice.Presenters
{
    public class FCMPresenter
    {
        FCMRepository repository;
        IFcmNotice fcmNotice;

        public FCMPresenter(IFcmNotice fcmNotice)
        {
            repository = new FCMRepository();
            this.fcmNotice = fcmNotice;
            
            this.fcmNotice.btnSend += sendClik;
            this.fcmNotice.cmboValChange += cmboChange;
        }
        public void sendClik(object sender,EventArgs e)
        {
            if(this.fcmNotice.txtTitle != null&&this.fcmNotice.txtMessage!=null)
            {
                //string json = "{\r\"to\":\"/" + this.fcmNotice.cmboTarget.SelectedValue+ "\",\r\n     \"priority\": \"high\",\r\n  \"data\": {\r\n    \"flag\": \"notice\",\r\n    \"title\" : \"" + this.fcmNotice.txtTitle.Text + "\",\r\n    \"message\":\"" + this.fcmNotice.txtMessage.Text + "\"\r\n  }\r\n}";
                string json = "{\"to\":\""+ this.fcmNotice.cmboTarget.SelectedValue + "\",\"priority\": \"high\",\"data\":{\"flag\":\"notice\",\"title\" :\""+ this.fcmNotice.txtTitle.Text + "\",\"message\":\""+ this.fcmNotice.txtMessage.Text + "\"}}";
                PostAsync("AAAAP7uKbqE:APA91bH-SIXQ3XlQswT9AWQEj7TW68H_YZWx9qUFp0GPVVmnRoNuygQLh-7uNGe79YIU_jJ4nOY3p04SiZtHZclMA2qQrs4qSZHGyATSnZ0RUChtSPeAe858CaQJoLYaZDcZkBl-mVa9", "https://fcm.googleapis.com/fcm/send", json);
            }
        }
        public void cmboChange(object sender,EventArgs e)
        {
            string stmt = "SELECT MemberID ,MemName,FCMtoken FROM tblMember where FCMtoken is not null";
            this.fcmNotice.cmboTarget.DataSource= repository.makeList(stmt);
            this.fcmNotice.cmboTarget.DisplayMember = "name";
            this.fcmNotice.cmboTarget.ValueMember = "fcmToken";

        }

        public async Task<JObject> PostAsync(string token, string url, string content)
        {
            byte[] data = Encoding.UTF8.GetBytes(content);
            WebRequest request = WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Headers.Add("Authorization", "key=" + token);
            request.ContentLength = data.Length;

            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            try
            {
                WebResponse response = await request.GetResponseAsync();
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    string responseContent = reader.ReadToEnd();
                    JObject adResponse =Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(responseContent);
                    return adResponse;
                }
            }
            catch (WebException webException)
            {
                if (webException.Response != null)
                {
                    using (StreamReader reader = new StreamReader(webException.Response.GetResponseStream()))
                    {
                        string responseContent = reader.ReadToEnd();
                        return Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(responseContent); ;
                    }
                }
            }

            return null;
        }
    }
}
