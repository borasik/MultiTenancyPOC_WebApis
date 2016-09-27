namespace DH.PaymentAllocationObjects
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [InRuleImportInclude()]
    [Serializable()]
    [DataContract]
    public class Error
    {
        
        private string _number;
        
        private string _message;

        [DataMember]
        public virtual string Number
        {
            get
            {
                return this._number;
            }
            set
            {
                this._number = value;
            }
        }

        [DataMember]
        public virtual string Message
        {
            get
            {
                return this._message;
            }
            set
            {
                this._message = value;
            }
        }
    }
}
