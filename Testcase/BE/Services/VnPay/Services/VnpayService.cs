using Microsoft.Extensions.Options;
using System.Net;
using System.Text;
using Repositories.VnPay.Config;
using Repositories.VnPay.Lib;
using Microsoft.AspNetCore.Http;
using API.Model.PaymentModel;
using API.Model.VnPayModel;
using SWP391Project.Services.Model.VnpayModel;
using Microsoft.Extensions.Caching.Memory;

namespace Repositories.VnPay.Services
{
    public class VnpayService
    {
        private VnpayPayResponse _vnpayPayResponse;
        private readonly VnpayConfig _vnpayConfig;
        private VnpayPayRequest _vnpayPayRequest;
        private readonly UnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;
        public VnpayService(VnpayPayResponse vnpayPayResponse, IOptions<VnpayConfig> vnpayConfig, VnpayPayRequest vnpayPayRequest, UnitOfWork unitOfWork, IMemoryCache cache)
        {
            _vnpayPayResponse = vnpayPayResponse;
            _vnpayConfig = vnpayConfig.Value;
            _vnpayPayRequest = vnpayPayRequest;
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        public SortedList<string, string> responseData
           = new SortedList<string, string>(new VnpayCompare());

        //hàm này để sắp các string theo thứ tự trừ trên xuống theo bảng chữ cái
        public SortedList<string, string> requestData
            = new SortedList<string, string>(new VnpayCompare());

        //Tạo link thanh toán VNPAY
        public string GetLink(string baseUrl, string secretKey)
        {
            MakeRequestData();
            StringBuilder data = new StringBuilder();
            foreach (KeyValuePair<string, string> kv in requestData)
            {
                if (!String.IsNullOrEmpty(kv.Value))
                {
                    data.Append(WebUtility.UrlEncode(kv.Key) + "=" + WebUtility.UrlEncode(kv.Value) + "&");
                }
            }

            string result = baseUrl + "?" + data.ToString();
            var secureHash = HashHelper.HmacSHA512(secretKey, data.ToString().Remove(data.Length - 1, 1));
            return result += "vnp_SecureHash=" + secureHash;
        }

        //Check data if it is not null then add to requestData
        public void MakeRequestData()
        {
            if (_vnpayPayRequest.vnp_Amount != null)
                requestData.Add("vnp_Amount", _vnpayPayRequest.vnp_Amount.ToString() ?? string.Empty);
            if (_vnpayPayRequest.vnp_Command != null)
                requestData.Add("vnp_Command", _vnpayPayRequest.vnp_Command);
            if (_vnpayPayRequest.vnp_CreateDate != null)
                requestData.Add("vnp_CreateDate", _vnpayPayRequest.vnp_CreateDate);
            if (_vnpayPayRequest.vnp_CurrCode != null)
                requestData.Add("vnp_CurrCode", _vnpayPayRequest.vnp_CurrCode);
            if (_vnpayPayRequest.vnp_BankCode != null)
                requestData.Add("vnp_BankCode", _vnpayPayRequest.vnp_BankCode);
            if (_vnpayPayRequest.vnp_IpAddr != null)
                requestData.Add("vnp_IpAddr", _vnpayPayRequest.vnp_IpAddr);
            if (_vnpayPayRequest.vnp_Locale != null)
                requestData.Add("vnp_Locale", _vnpayPayRequest.vnp_Locale);
            if (_vnpayPayRequest.vnp_OrderInfo != null)
                requestData.Add("vnp_OrderInfo", _vnpayPayRequest.vnp_OrderInfo);
            if (_vnpayPayRequest.vnp_OrderType != null)
                requestData.Add("vnp_OrderType", _vnpayPayRequest.vnp_OrderType);
            if (_vnpayPayRequest.vnp_ReturnUrl != null)
                requestData.Add("vnp_ReturnUrl", _vnpayPayRequest.vnp_ReturnUrl);
            if (_vnpayPayRequest.vnp_TmnCode != null)
                requestData.Add("vnp_TmnCode", _vnpayPayRequest.vnp_TmnCode);
            if (_vnpayPayRequest.vnp_ExpireDate != null)
                requestData.Add("vnp_ExpireDate", _vnpayPayRequest.vnp_ExpireDate);
            if (_vnpayPayRequest.vnp_TxnRef != null)
                requestData.Add("vnp_TxnRef", _vnpayPayRequest.vnp_TxnRef);
            if (_vnpayPayRequest.vnp_Version != null)
                requestData.Add("vnp_Version", _vnpayPayRequest.vnp_Version);
        }



        //Check Signature response from VNPAY
        public bool IsValidSignature(string secretKey)
        {
            MakeResponseData();
            StringBuilder data = new StringBuilder();
            foreach (KeyValuePair<string, string> kv in responseData)
            {
                if (!String.IsNullOrEmpty(kv.Value))
                {

                    data.Append(WebUtility.UrlEncode(kv.Key) + "=" + WebUtility.UrlEncode(kv.Value) + "&");
                }
            }
            if (data.Length > 0)
            {
                data.Remove(data.Length - 1, 1);
            }
            string checkSum = HashHelper.HmacSHA512(secretKey,
                data.ToString());
            return checkSum.Equals(_vnpayPayResponse.vnp_SecureHash, StringComparison.InvariantCultureIgnoreCase);
        }

        public void MakeResponseData()
        {
            if (_vnpayPayResponse.vnp_Amount != null)
                responseData.Add("vnp_Amount", _vnpayPayResponse.vnp_Amount.ToString() ?? string.Empty);
            if (!string.IsNullOrEmpty(_vnpayPayResponse.vnp_TmnCode))
                responseData.Add("vnp_TmnCode", _vnpayPayResponse.vnp_TmnCode.ToString() ?? string.Empty);
            if (!string.IsNullOrEmpty(_vnpayPayResponse.vnp_BankCode))
                responseData.Add("vnp_BankCode", _vnpayPayResponse.vnp_BankCode.ToString() ?? string.Empty);
            if (!string.IsNullOrEmpty(_vnpayPayResponse.vnp_BankTranNo))
                responseData.Add("vnp_BankTranNo", _vnpayPayResponse.vnp_BankTranNo.ToString() ?? string.Empty);
            if (!string.IsNullOrEmpty(_vnpayPayResponse.vnp_CardType))
                responseData.Add("vnp_CardType", _vnpayPayResponse.vnp_CardType.ToString() ?? string.Empty);
            if (!string.IsNullOrEmpty(_vnpayPayResponse.vnp_OrderInfo))
                responseData.Add("vnp_OrderInfo", _vnpayPayResponse.vnp_OrderInfo.ToString() ?? string.Empty);
            if (!string.IsNullOrEmpty(_vnpayPayResponse.vnp_TransactionNo))
                responseData.Add("vnp_TransactionNo", _vnpayPayResponse.vnp_TransactionNo.ToString() ?? string.Empty);
            if (!string.IsNullOrEmpty(_vnpayPayResponse.vnp_TransactionStatus))
                responseData.Add("vnp_TransactionStatus", _vnpayPayResponse.vnp_TransactionStatus.ToString() ?? string.Empty);
            if (!string.IsNullOrEmpty(_vnpayPayResponse.vnp_TxnRef))
                responseData.Add("vnp_TxnRef", _vnpayPayResponse.vnp_TxnRef.ToString() ?? string.Empty);
            if (!string.IsNullOrEmpty(_vnpayPayResponse.vnp_PayDate))
                responseData.Add("vnp_PayDate", _vnpayPayResponse.vnp_PayDate.ToString() ?? string.Empty);
            if (!string.IsNullOrEmpty(_vnpayPayResponse.vnp_ResponseCode))
                responseData.Add("vnp_ResponseCode", _vnpayPayResponse.vnp_ResponseCode ?? string.Empty);
        }

        //Create payment (save PaymentDtos to Database with Mapper)
        public string CreatePayment(RequestCreateVnpay requestCreateVnpay)
        {   
            if(requestCreateVnpay.RequiredAmount <5000 || requestCreateVnpay.RequiredAmount > 1000000000)
            {
                return "Valid amount ranges from 5,000 to under 1 billion VND";
            }

            /*string verificationCode = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6).ToUpper();
            _cache.Set(verificationCode, requestCreateVnpay, DateTime.Now.AddMinutes(15));*/
            var payment = requestCreateVnpay.ToPaymentEntity((int)requestCreateVnpay.userId, (int)requestCreateVnpay.requirementId);
            payment.Status = "Pending ";
            _unitOfWork.PaymentRepository.Insert(payment);
            _unitOfWork.Save();
            var resultPayment = _unitOfWork.PaymentRepository.Get(filter: x => x.Equals(payment)).FirstOrDefault();
            var paymentUrl = string.Empty;
            var test = _vnpayConfig;
            _vnpayPayRequest = new VnpayPayRequest(_vnpayConfig.Version,
            _vnpayConfig.TmnCode, DateTime.Now, "127.0.0.1" ?? string.Empty, requestCreateVnpay.RequiredAmount , requestCreateVnpay.PaymentCurrency ?? string.Empty,
                            "other", requestCreateVnpay.PaymentContent ?? string.Empty, _vnpayConfig.ReturnUrl, resultPayment.PaymentId!.ToString() ?? string.Empty);
            paymentUrl = GetLink(_vnpayConfig.PaymentUrl, _vnpayConfig.HashSecret);
            return paymentUrl;
        }

        public ResponseMessage checkPayment(IQueryCollection collections)
        {

            var vnpay = new VnPayLibrary();
            foreach (var (key, value) in collections)
            {
                if (!string.IsNullOrEmpty(key) && key.StartsWith("vnp_"))
                {
                    vnpay.AddResponseData(key, value.ToString());
                }
            }

            var vnp_SecureHash = collections.FirstOrDefault(p => p.Key == "vnp_SecureHash").Value;
            bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, _vnpayConfig.HashSecret);
           /* _cache.TryGetValue(vnpay.GetResponseData("vnp_TxnRef"), out RequestCreateVnpay requestCreateVnpay);
            var payment = requestCreateVnpay.ToPaymentEntity((int)requestCreateVnpay.userId, (int)requestCreateVnpay.requirementId);*/
           var payment = _unitOfWork.PaymentRepository.GetByID(int.Parse(vnpay.GetResponseData("vnp_TxnRef")));
            //_cache.Remove(vnpay.GetResponseData("vnp_TxnRef"));
            if (checkSignature)
            {
                if (payment != null)
                {
                    if(vnpay.GetResponseData("vnp_ResponseCode") == "00")
                    {
                        return new ResponseMessage()
                        {

                            ResponseCode = vnpay.GetResponseData("vnp_ResponseCode"),
                            Message = "Confirm Success",
                            Payment = payment,
                        };
                    }
                }
                else
                {
                    return new ResponseMessage()
                    {
                        ResponseCode = vnpay.GetResponseData("vnp_ResponseCode"),
                        Message = "Có lỗi xảy ra trong quá trình xử lý",
                        Payment = payment,
                    }; // "RspCode":"01"
                }
            }
            return new ResponseMessage()
            {
                ResponseCode = vnpay.GetResponseData("vnp_ResponseCode"),
                Message = "Có lỗi xảy ra trong quá trình xử lý",
                Payment = payment,
            };
        }
    }
}
