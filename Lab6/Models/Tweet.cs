using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;


namespace Lab6.Models
{
    public class Tweet
    {
        /// <summary>
        /// Database id of the tweet
        /// </summary>
        public int TweetId
        {
            get;
            set;
        }

        [Display(Name = "Username")]
        /// <summary>
        /// The person who created the tweet
        /// </summary>
        public string Username
        {
            get;
            set;
        }

        [Display(Name = "Content")]
        /// <summary>
        /// The content of the tweet
        /// </summary>
        public string Content
        {
            get;
            set;
        }
    }
}