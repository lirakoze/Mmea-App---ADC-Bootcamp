using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MmeaAppADC.Models
{
    public class SMSAndCallService
    {
        public SMSAndCallService()
        {

        }
        public async Task SendSMS(Message message)
        {
            try
            {
                await Sms.ComposeAsync(new SmsMessage(message.Content, message.VetPhoneNo));
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }
        public void PhoneDial(string number)
        {
            try
            {
                PhoneDialer.Open(number);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }
    }
}
