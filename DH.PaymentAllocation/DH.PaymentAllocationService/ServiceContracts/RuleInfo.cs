namespace DH.PaymentAllocationService
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using DH.PaymentAllocationObjects;

    [Serializable()]
    [DataContract]
    public class RuleInfo
    {
        private string _ruleName;

        private string _caller;

        private string _status;

        private List<Error> _errorDetails = new List<Error>();

        private string _ruleEvaluationDate;

        private string _filler;

        [DataMember]
        public virtual string RuleName
        {
            get { return this._ruleName; }
            set { this._ruleName = value; }
        }

        [DataMember]
        public virtual string Caller
        {
            get { return this._caller; }
            set { this._caller = value; }
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
        public virtual string RuleEvaluationDate
        {
            get { return this._ruleEvaluationDate; }
            set { this._ruleEvaluationDate = value; }
        }

        [DataMember]
        public virtual string Filler
        {
            get { return this._filler; }
            set { this._filler = value; }
        }
    }
}