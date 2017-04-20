using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tweetinvi;

namespace TwitterMarketing
{
    class Program
    {
        static void Main(string[] args)
        {
            Authenticate();
            GetListToFollow(new [] {"housing","magicbricks"});
        }

        static void Authenticate()
        {
            string at = "";
            string ats = "";
            string ck = "";
            string cs = "";
            // Set up your credentials (https://apps.twitter.com)
            Auth.SetUserCredentials(ck, cs, at, ats);
        }

        static void GetListToFollow(string[] handles)
        {
            foreach (var handle in handles)
            {
                var user = User.GetUserFromScreenName(handle);
                var followers = user.Followers;
                foreach (var follower in followers)
                {
                    // Given handles, get list of followers, and # folks they follow, avg tweet interval, last tweet date
                    Console.WriteLine("{0}\t{1}\t{2}",follower.ScreenName, follower.FollowersCount, follower.Name);
                    Thread.Sleep(1000);
                }
            }
        }

        static void FollowUsers(string file)
        {
            var handles = File.ReadAllLines(file);
            // Given the list, follow all the handles
            foreach (var handle in handles)
            {
                var myself = User.GetAuthenticatedUser();
                var followed = myself.FollowUser(handle);
                Thread.Sleep(1000);
            }
        }

        static void CheckTimelineAndSendTweet()
        {
            var myself = User.GetAuthenticatedUser();
            var tweets = myself.GetHomeTimeline();
            tweets = tweets.Where(t => (DateTime.Now - t.CreatedAt).TotalMinutes < 5);
            foreach (var tweet in tweets)
            {
                var user = tweet.CreatedBy.ScreenName;
                Tweet.PublishTweet(string.Format("@{0} India's most authoritative app for property valuation. Download at ....",user));
            }
            // Keep checking timeline and send a tweet to those who tweet
        }

        static void SMSMarketing()
        {
            // Use TextLocal - Indian stuff and fairly inexpensive.. But, beware of spamming causing lower ratings to the app
            // Easy API with Text local - roughly 30p/sms 
        }

        static void GetPhoneNumbersAndNames()
        {            
        }
    }
}
