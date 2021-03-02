using BOA.Types.Banking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BOA.Connector.Banking
{

    //typeof(T).Name // class name, 
    //typeof(T).FullName // namespace and class name
    //typeof(T).Namespace // namespace
    //var a = typeof(T);

    public class Connect<Response,Request>where Response:ResponseBase,new() where Request:RequestBase,new()
    {

        //public Response Execute<T>(T request) where T : RequestBase,new() --- BUDA KULLANILABİLİR
        public Response Execute (Request request) 
        {
            
            Response response= new Response();

            //string Name = typeof(Request).FullName.Replace("Types", "Process").Replace("Request", string.Empty);
            //string MethodName = (string)typeof(Request).GetProperty("MethodName").GetValue(request);

            var assembly = Assembly.LoadFrom("C:/Users/sakir/Desktop/BOA.BANKING/BOA.Process.Banking/bin/Debug/BOA.Process.Banking.dll"); //1

            var getType = assembly.GetType(typeof(Request).FullName.Replace("Types", "Process").Replace("Request", string.Empty)); //2
            
            var method = getType.GetMethod((string)typeof(Request).GetProperty("MethodName").GetValue(request)); //3

            var activator = Activator.CreateInstance(getType); //4

            response = (Response)method.Invoke(activator,new object[] {request}) ; //5

            return response;

        }

        //Type AS = typeof(Request);

        //string deneme = type.Name;//Name  class ismini getiriyor 
        //var al = new BOA.Process.Banking.Customer();
        //Type type1 = al.GetType();


        /*önemli deneme*/
        //var assembly = Assembly.LoadFrom("C:/Users/sakir/Desktop/BOA.BANKING/BOA.Process.Banking/bin/Debug/BOA.Process.Banking.dll").
        //               GetType(typeof(Request).FullName.Replace("Types", "Process").Replace("Request", string.Empty)).
        //               GetMethod((string)typeof(Request).GetProperty("MethodName").GetValue(request)).Invoke()




        //var pr = new BOA.Process.Banking.User().GetType();

        //var ins = Activator.CreateInstance(pr);

        //var OFF = ins.GetType().GetMethod(Name).Invoke(ins,(AS)request);


        //if (type.Namespace == "BOA.Types.Banking")

        //Type type = request.GetType();


        /* ASIIL ÇALIŞAN BU*/
        //string processName = request.GetType().FullName.Replace("Types", "Process").Replace("Request", string.Empty) +"."+ request.GetType().GetProperty("MethodName").GetValue(request); // namespace ile birlikte geliyor

        //Type type = this.GetType();
        //type.GetMethod(processName);

        //var pr = new BOA.Process.Banking.User();

        //Type pr = new BOA.Process.Banking.User((UserRequest)request).GetType();


        //var activator = (BOA.Process.Banking.User)Activator.CreateInstance(pr);

        //var getMethod = pr.GetMethod(request.MethodName);


        //response  = (ResponseBase)getMethod.Invoke(activator, null);


        //var a = pr.DeclaringMethod(request.MethodName, BindingFlags.Public);

        //MethodInfo info = pr.GetMethod(request.MethodName, BindingFlags.Public);
        //response;

        //if (request.MethodName == "User_Login")
        //{

        //response = pr.User_Login((RequestBase.GetType())request);
        //}



        //    if (processName== "BOA.Process.Banking.ParameterType")
        //    {
        //        var pr = new BOA.Process.Banking.ParameterType();

        //        if (request.MethodName == "ParameterTypeNameRead")
        //        {
        //            response = pr.ParameterTypeNameRead((ParameterTypeRequest)request);
        //        }


        //    }



        //    if (processName == "BOA.Process.Banking.Customer")
        //    {
        //        var pr = new BOA.Process.Banking.Customer();

        //        if (request.MethodName == "CustomerSave")
        //        {

        //            response = pr.CustomerSave((CustomerRequest)request);
        //        }
        //        if (request.MethodName == "CustomerUpdate")
        //        {

        //            response = pr.CustomerUpdate((CustomerRequest)request);
        //        }
        //        if (request.MethodName == "CustomerFilterRead")
        //        {

        //            response = pr.CustomerFilterRead((CustomerRequest)request);
        //        }
        //        if (request.MethodName == "CusDelete")
        //        {

        //            response = pr.CusDelete((CustomerRequest)request);
        //        }
        //        if (request.MethodName == "CustomerRead")
        //        {

        //            response = pr.CustomerRead((CustomerRequest)request);
        //        }

        //        //ADRES-TELEFON-EMAİL EKSTRA EKLEME
        //        if (request.MethodName == "NumberAdd")
        //        {

        //            response = pr.NumberAdd((CustomerRequest)request);
        //        }
        //        if (request.MethodName == "EpostaAdd")
        //        {

        //            response = pr.EpostaAdd((CustomerRequest)request);
        //        }
        //        if (request.MethodName == "AdressAdd")
        //        {

        //            response = pr.AdressAdd((CustomerRequest)request);
        //        }
        //        //ADRES-TELEFON-EMAİL EKSTRA EKLEME


        //        //adres email telefon okuma
        //        if (request.MethodName == "CustomerContactRead")
        //        {

        //            response = pr.CustomerContactRead((CustomerRequest)request);
        //        }
        //        //adres email telefon okuma

        //        //adres e mail e posta silme
        //        if (request.MethodName == "phoneNumberDelete")
        //        {

        //            response = pr.phoneNumberDelete((CustomerRequest)request);
        //        }
        //        if (request.MethodName == "mailDelete")
        //        {

        //            response = pr.mailDelete((CustomerRequest)request);
        //        }
        //        if (request.MethodName == "adresDelete")
        //        {

        //            response = pr.adresDelete((CustomerRequest)request);
        //        }
        //        //adres e mail e posta silme


        //        //hesap açma ek no
        //        if (request.MethodName == "CustomerParameter_AccountNumber_Read")
        //        {

        //            response = pr.CustomerParameter_AccountNumber_Read((CustomerRequest)request);
        //        }

        //        if (request.MethodName == "CustomerParameter_Add")
        //        {

        //            response = pr.CustomerParameter_Add((CustomerRequest)request);
        //        }

        //        if (request.MethodName == "CustomerParameter_Delete")
        //        {

        //            response = pr.CustomerParameter_Delete((CustomerRequest)request);
        //        }

        //        if (request.MethodName == "AccountBalance_Update")
        //        {

        //            response = pr.AccountBalance_Update((CustomerRequest)request);
        //        }
        //    }











        //if (type.Namespace == "BOA.Types.Banking.UserRequest")
        //{
        //    var pr = new BOA.Process.Banking.User();

        //    if (request.MethodName == "User_Login")
        //    {
        //        response = pr.User_Login((UserRequest)request);
        //    }


        //}
        //if (type.Namespace == "BOA.Types.Banking.CustomerRequest")
        //{
        //    var pr = new BOA.Process.Banking.Customer();
        //    if (request.MethodName == "CustomerSave")
        //    {
        //        response = pr.CustomerSave((CustomerRequest)request);
        //    }
        //}
    }
}
