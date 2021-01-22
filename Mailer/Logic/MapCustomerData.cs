using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using Mailer.Dto;
using MySql.Data.MySqlClient;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Mailer.Logic
{
    public class MapCustomerData: IMapCustomerData
    {
        // can be changed in the web config
        private string SenderEmailAddress = ConfigurationManager.AppSettings["SenderEmailAddress"]; 
        private string RecipientEmailAddress = ConfigurationManager.AppSettings["RecipientEmailAddress"]; 
        private bool isTestSend = Convert.ToBoolean(ConfigurationManager.AppSettings["isTestSend"]);


        public async Task<bool> SendEmailLogic(CustomerDto CustomerDto)
        {

            string apiKey = " "; //ADD API KEY

            SendGridClient Client = new SendGridClient(apiKey);

            var CustomersInDb = await GetList();

            if (CustomersInDb == null) return false;

            foreach (var customer in CustomersInDb)
            {
                 customer.Email = isTestSend ? RecipientEmailAddress : customer.Email;

                var dynamicTemplateData = new Dictionary<string, object>
                { // dynamicTemplateData fields goes into the email template eg on html {{Name}} will have the value of Name below.
                    { "Id", customer.Id },                   //1
                    { "Name", customer.Name },               //2
                    { "Time", customer.Time },               //3
                    { "email", customer.Email },             //4
                    { "Description", customer.Description }, //5
                    { "Cell", customer.Cell },               //6
                };

                SendGridMessage msg = MailHelper.CreateSingleTemplateEmail(
                    new EmailAddress(SenderEmailAddress, "ACME Info Tech"),
                    new EmailAddress(customer.Email), "d-template-id", dynamicTemplateData); 


                switch (CustomerDto.Campaign)
                {//SetTemplateId ARE EMAIL TEMPLATE IDs LOCATED ON SENDGRID
                    case "Maintenance":
                        msg.SetTemplateId("d-7ddbf1d167f84b219460d81daf1ef79d"); 
                        break;
                    case "Orders":
                        msg.SetTemplateId("d-a612e58c3fb74832a9460d327f46d871");
                        break;
                }

                Response response = await Client.SendEmailAsync(msg); // POST PAYLOAD TO SENDGRID

                CustomerDto.EmailSent = (response.StatusCode.ToString() != "Accepted") ? false : true;

                // TODO: save this record once sent

                if (response.StatusCode.ToString() != "Accepted")
                {
                    //return false;
                }
                else
                {
                    //return true;
                }
            }


            return true;
           
        }

        // Connect to remote MySql server and retrieve a list of user details 
        public async Task<IEnumerable<CustomerDto>> GetList()
        {
            
            using (MySqlConnection con = new MySqlConnection("SERVER=000.000.00.00;" + "DATABASE=dbName;" + "UID=user1;" + "Pwd=1234;"))
            {
                con.Open();
                string s = "select * from cData";
                using (MySqlCommand cmd = new MySqlCommand(s, con))
                {
                    await cmd.ExecuteNonQueryAsync();
                    MySqlDataReader reader = cmd.ExecuteReader();

                    List<CustomerDto> list = new List<CustomerDto>();
                    while (reader.Read())
                    {
                        var record = new CustomerDto
                        {
                            Id = Convert.ToInt32(reader["Id"].ToString()),
                            Time = DateTime.Parse(reader["Time"].ToString()),
                            Name = reader["Name"].ToString(),
                            Email = reader["eMail"].ToString(),
                            Cell = Convert.ToInt64(reader["Cell"].ToString()),
                            Description = reader["Type"].ToString()
                        };

                        list.Add(record);
                    }

                    reader.Close();
                    con.Close();

                    return list;
                }
            }
        } //Working [Note IP needs to be added on cpanel remote mySql]
    }
}