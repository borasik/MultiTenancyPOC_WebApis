namespace DH.PaymentAllocationObjects
{
    using System;
    using System.Xml.Serialization;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [InRuleImportInclude()]
    [Serializable()]
    [DataContract]
    public class PaymentAllocationRoot
    {

        private string _ruleEvaluationDate;

        private decimal _paymentAmount;
        
        private List<Loan> _loanSet = new List<Loan>();

        private List<Payment> _payments = new List<Payment>();
        
        private List<Error> _errorDetails = new List<Error>();
        
        private string _filler;

        public virtual string RuleEvaluationDate
        {
            get
            {
                return this._ruleEvaluationDate;
            }
            set
            {
                this._ruleEvaluationDate = value;
            }
        }

        [DataMember]
        public virtual decimal PaymentAmount
        {
            get
            {
                return this._paymentAmount;
            }
            set
            {
                this._paymentAmount = value;
            }
        }

        [DataMember]
        public virtual List<Loan> LoanSet
        {
            get
            {
                return this._loanSet;
            }
        }

        [DataMember]
        public virtual List<Payment> Payments
        {
            get
            {
                return this._payments;
            }
        }

        [DataMember]
        public virtual List<Error> ErrorDetails
        {
            get
            {
                return this._errorDetails;
            }
        }

        [DataMember]
        public virtual string Filler
        {
            get
            {
                return this._filler;
            }
            set
            {
                this._filler = value;
            }
        }
    }
}
