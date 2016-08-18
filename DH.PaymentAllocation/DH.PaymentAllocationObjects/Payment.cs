namespace DH.PaymentAllocationObjects
{
    using System;
    using System.Xml.Serialization;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [InRuleImportInclude()]
    [Serializable()]
    [DataContract]
    public class Payment
    {

        private string _loanNumber;

        private decimal _interestPayment;
        
        private decimal _principalPayment;
        
        private decimal _portionAllocation;

        [DataMember]
        public virtual string LoanNumber
        {
            get
            {
                return this._loanNumber;
            }
            set
            {
                this._loanNumber = value;
            }
        }

        [DataMember]
        public virtual decimal InterestPayment
        {
            get
            {
                return this._interestPayment;
            }
            set
            {
                this._interestPayment = value;
            }
        }

        [DataMember]
        public virtual decimal PrincipalPayment
        {
            get
            {
                return this._principalPayment;
            }
            set
            {
                this._principalPayment = value;
            }
        }

        [DataMember]
        public virtual decimal PortionAllocation
        {
            get
            {
                return this._portionAllocation;
            }
            set
            {
                this._portionAllocation = value;
            }
        }
    }
}
