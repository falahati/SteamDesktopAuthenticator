using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamAuth
{
    public class Confirmation
    {
        /// <summary>
        /// The ID of this confirmation
        /// </summary>
        public ulong ID;

        /// <summary>
        /// The unique key used to act upon this confirmation.
        /// </summary>
        public ulong Key;

        /// <summary>
        /// The value of the data-type HTML attribute returned for this contribution.
        /// </summary>
        public int IntType;

        /// <summary>
        /// Represents either the Trade Offer ID or market transaction ID that caused this confirmation to be created.
        /// </summary>
        public ulong Creator;

        /// <summary>
        /// The type of this confirmation.
        /// </summary>
        public ConfirmationType ConfType;
        
        public string Icon { get; set; }
        
        public string Name { get; set; }
        
        public string Summary { get; set; }
        
        public DateTime Created { get; set; }

        public Confirmation(ulong id, ulong key, int type, ulong creator, string icon, string name, IEnumerable<string> summary, uint created)
        {
            this.ID = id;
            this.Key = key;
            this.IntType = type;
            this.Creator = creator;
            this.Icon = icon;
            this.Name = name;
            this.Summary = string.Join("\r\n", summary.ToArray());
            this.Created = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            this.Created = this.Created.AddSeconds(created).ToLocalTime();

            //Do a switch simply because we're not 100% certain of all the possible types.
            switch (type)
            {
                case 1:
                    this.ConfType = ConfirmationType.GenericConfirmation;
                    break;
                case 2:
                    this.ConfType = ConfirmationType.Trade;
                    break;
                case 3:
                    this.ConfType = ConfirmationType.MarketSellTransaction;
                    break;
                default:
                    this.ConfType = ConfirmationType.Unknown;
                    break;
            }
        }

        public enum ConfirmationType
        {
            GenericConfirmation,
            Trade,
            MarketSellTransaction,
            Unknown
        }
    }
}
