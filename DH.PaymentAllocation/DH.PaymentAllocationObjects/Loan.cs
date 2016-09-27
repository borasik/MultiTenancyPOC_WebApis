namespace DH.PaymentAllocationObjects
{
    using System;
    using System.Xml.Serialization;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    
    [InRuleImportInclude()]
    [Serializable()]
    [DataContract]
    public class Loan
    {
        
        private string _loanNumber;

        private decimal _paymentAmount;
        
        private string _loanStatus;
        
        private string _scheduledPaymentAmount;
        
        private decimal _accruedInterest;

        private decimal _accruingNonRepaymentInterest;
        
        private decimal _principalBalance;

        private string _parentAccountNumber;
        
        private string _issuingProvince;
        
        private string _integratedLoanType;

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
        public virtual string LoanStatus
        {
            get
            {
                return this._loanStatus;
            }
            set
            {
                this._loanStatus = value;
            }
        }

        [DataMember]
        public virtual decimal ScheduledPaymentAmount
        {
            get
            {
                decimal retVal;
                return decimal.TryParse(this._scheduledPaymentAmount, out retVal) ? (decimal)retVal : 0;
            }
            set
            {
                this._scheduledPaymentAmount = value.ToString();
            }
        }

        [DataMember]
        public virtual decimal AccruedInterest
        {
            get
            {
                return this._accruedInterest;
            }
            set
            {
                this._accruedInterest = value;
            }
        }

        [DataMember]
        public virtual decimal AccruingNonRepaymentInterest
        {
            get
            {
                return this._accruingNonRepaymentInterest;
            }
            set
            {
                this._accruingNonRepaymentInterest = value;
            }
        }

        [DataMember]
        public virtual decimal PrincipalBalance
        {
            get
            {
                return this._principalBalance;
            }
            set
            {
                this._principalBalance = value;
            }
        }

        [DataMember]
        public virtual string ParentAccountNumber
        {
            get
            {
                return this._parentAccountNumber;
            }
            set
            {
                this._parentAccountNumber = value;
            }
        }

        [DataMember]
        public virtual string IssuingProvince
        {
            get
            {
                return this._issuingProvince;
            }
            set
            {
                this._issuingProvince = value;
            }
        }

        [DataMember]
        public virtual string IntegratedLoanType
        {
            get
            {
                return this._integratedLoanType;
            }
            set
            {
                this._integratedLoanType = value;
            }
        }
    }
}
