using System;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace DH.PaymentAllocationService
{
    public class ServiceData
    {
        [DataMember]
        public bool Result { get; set; }

        [DataMember]
        public string ErrorMessage { get; set; }

        [DataMember]
        public string ErrorDetails { get; set; }
    }
}
