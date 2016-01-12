using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankOfBIT.Models
{
    /// <summary>
    /// Model a Bank account status class
    /// </summary>
    public class AccountStatus
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int AccountStatusId { get; set; }

        [Required]
        [Display(Name="Account\nStatus")]
        public string Description { get; set; }

        /// <summary>
        /// adjusting rate for different account
        /// </summary>
        /// <returns>rate</returns>
        public virtual double RateAdjustment()
        {
            return 0;
        }
    }

    /// <summary>
    /// Model a Client class
    /// </summary>
    public class Client
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ClientId { get; set; }

        [Required]
        [RegularExpression("[0-9]{8}", ErrorMessage = "Value must between 10000000-99999999.")]
        [Display(Name = "Client")]
        public int ClientNumber { get; set; }

        [Required]
        [StringLength(35, MinimumLength = 1)]
        [Display(Name = "First\nName")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(35, MinimumLength = 1)]
        [Display(Name = "Last\nName")]
        public string LastName { get; set; }

        [Required]
        [StringLength(35, MinimumLength = 1)]
        [Display(Name = "Street Address")]
        public string Address { get; set; }

        [Required]
        [StringLength(35, MinimumLength = 1)]
        public string City { get; set; }

        [Required]
        [RegularExpression("[A-Z][A-Z]",ErrorMessage = "Must be 2 uppercase characters.")]
        public string Province { get; set; }

        [Required]
        [StringLength(7)]
        [RegularExpression("[A-Z][0-9][A-Z] [0-9][A-Z][0-9]",ErrorMessage = "Must be 7 characters like\"A9A 9A9\"")]
        [Display(Name = "Postal\nCode")]
        public string PostalCode { get; set; }

        [Required]
        [Display(Name = "Created\nOn")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DateCreated { get; set; }

        [Display(Name = "Special\nClient Notes")]
        public string Notes { get; set; }

        [Display(Name = "Name")]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        [Display(Name = "Address")]
        public string FullAddress
        {
            get
            {
                return Address + " " + City + "," + Province + " " + PostalCode;
            }
        }

        //lazy loading
        public virtual ICollection<BankAccount> BankAccount { get; set; }

        /// <summary>
        /// Set next client number
        /// </summary>
        public void SetNextClientNumber() { }
    }

    /// <summary>
    /// Model a Bank account class
    /// </summary>
    public abstract class BankAccount
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int BankAccountId { get; set; }

        [Required]
        [Display(Name = "Account\nNumber")]
        [RegularExpression("[0-9]+")]
        public int AccountNumber { get; set; }

        [Required]
        [ForeignKey("Client")]
        public int ClientId { get; set; }

        [Required]
        [ForeignKey("AccountStatus")]
        public int AccountStatusId { get; set; }

        [Required]
        [Display(Name = "Current\nBalance")]
        [DisplayFormat(DataFormatString="{0:c}")]
        public double Balance { get; set; }

        [Required]
        [Display(Name = "Opening\nBalance")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double OpeningBalance { get; set; }

        [Required]
        [Display(Name = "Created\nOn")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DateCreated { get; set; }

        [Display(Name = "Special\nAccount Notes")]
        public string Notes { get; set; }

        /// <summary>
        /// Set next account numbers
        /// </summary>
        public abstract void SetNexAccountNumber();

        //lazy loading
        public virtual AccountStatus AccountStatus { get; set; }

        //lazy loading
        public virtual Client Client { get; set; }
    }


    #region Sub classes of BankAccount

    /// <summary>
    /// Model a saving account class
    /// inherit bannk account class
    /// </summary>
    public class SavingsAccount : BankAccount
    {
        [Required]
        [Display(Name = "Service\nCharges")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double SavingsServiceCharges { get; set; }

        /// <summary>
        /// Set next account numbers
        /// </summary>
        public override void SetNexAccountNumber() { }
    }

    /// <summary>
    /// Model a mortgage account class
    /// inherit bannk account class
    /// </summary>
    public class MortgageAccount : BankAccount
    {
        [Required]
        [Display(Name = "Mortgage\nRate")]
        [DisplayFormat(DataFormatString = "{0:p}")]
        public double MortgageRate { get; set; }

        public int Ammortization { get; set; }

        /// <summary>
        /// Set next account numbers
        /// </summary>
        public override void SetNexAccountNumber() { }
    }

    /// <summary>
    /// Model a investment account class
    /// inherit bannk account class
    /// </summary>
    public class InvestmentAccount : BankAccount
    {
        [Required]
        [Display(Name = "Interest\nRate")]
        [DisplayFormat(DataFormatString = "{0:p}")]
        public double InterestRate { get; set; }

        /// <summary>
        /// Set next account numbers
        /// </summary>
        public override void SetNexAccountNumber() { }
    }

    /// <summary>
    /// Model a chequing account class
    /// inherit bannk account class
    /// </summary>
    public class ChequingAccount : BankAccount
    {
        [Required]
        [Display(Name = "Service\nCharges")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double ChequingServiceCharges { get; set; }

        /// <summary>
        /// Set next account numbers
        /// </summary>
        public override void SetNexAccountNumber() { }
    }

    #endregion

    #region Sub classes for AccountStatus

    /// <summary>
    /// Model a active bank status 
    /// </summary>
    public class ActiveStatus : AccountStatus
    {
        /// <summary>
        /// Adjusting rate
        /// </summary>
        /// <returns>rate</returns>
        public override double RateAdjustment()
        {
            return base.RateAdjustment();
        }
    }

    /// <summary>
    /// Model a inactive bank status
    /// </summary>
    public class InactiveStatus : AccountStatus
    {
        /// <summary>
        /// Adjusting rate
        /// </summary>
        /// <returns>rate</returns>
        public override double RateAdjustment()
        {
            return 0;
        }
    }

    /// <summary>
    /// Model a delinquent bank status
    /// </summary>
    public class DelinquentStatus : AccountStatus
    {
        /// <summary>
        /// Adjusting rate
        /// </summary>
        /// <returns>rate</returns>
        public override double RateAdjustment()
        {
            return 0;
        }
    }

    /// <summary>
    /// Model a frozen bank status
    /// </summary>
    public class FrozenStatus : AccountStatus
    {
        /// <summary>
        /// Adjusting rate
        /// </summary>
        /// <returns>rate</returns>
        public override double RateAdjustment()
        {
            return 0;
        }
    }

    /// <summary>
    /// Model a closed bank status
    /// </summary>
    public class ClosedStatus : AccountStatus
    {
        /// <summary>
        /// Adjusting rate
        /// </summary>
        /// <returns>rate</returns>
        public override double RateAdjustment()
        {
            return 0;
        }
    }

    #endregion
   
}