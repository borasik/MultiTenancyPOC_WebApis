namespace DH.PaymentAllocationService
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using DH.PaymentAllocationObjects;

    [Serializable()]
    [DataContract]
    public class InRuleRequest
    {
        private RuleInfo _ruleInfo;
        private string _status;
        private List<Error> _errorDetails = new List<Error>();
        private string _ruleDataXml;

        [DataMember]
        public virtual RuleInfo RuleInfo
        {
            get { return this._ruleInfo; }
            set { this._ruleInfo = value; }
        }

        [DataMember]
        public virtual string Status
        {
            get { return this._status; }
            set { this._status = value; }
        }

        [DataMember]
        public virtual List<Error> ErrorDetails
        {
            get { return this._errorDetails; }
            set { this._errorDetails = value; }
        }

        [DataMember]
        public virtual string RuleDataXML
        {
            get { return this._ruleDataXml; }
            set { this._ruleDataXml = value; }
        }
    }
}