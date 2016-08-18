namespace DH.PaymentAllocationService
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using DH.PaymentAllocationObjects;

    [Serializable()]
    [DataContract]
    public class InRuleResponse
    {

        private RuleInfo _ruleInfo;
        private string _ruleResult;

        [DataMember]
        public virtual RuleInfo RuleInfo
        {
            get
            {
                return this._ruleInfo;
            }
            set
            {
                this._ruleInfo = value;
            }
        }

        [DataMember]
        public virtual string RuleResult
        {
            get
            {
                return this._ruleResult;
            }
            set
            {
                this._ruleResult = value;
            }
        }
    }
}
