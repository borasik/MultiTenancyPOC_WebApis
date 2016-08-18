using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DH.PaymentAllocationObjects;

namespace DH.PaymentAllocationService
{
    [ServiceContract(Namespace = Constants.Namespace)]
    public interface IInRuleService
    {
        [OperationContract]
        InRuleResponse PaymentAllocationRules(InRuleRequest request);
    }
}
