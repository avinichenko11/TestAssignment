using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using FareportalTestAssignment.Responses;
using NUnit.Framework;
using RestClient.Core;
using TechTalk.SpecFlow;

namespace FareportalTestAssignment.Tests.StepDefinitions
{
    [Binding]
    public sealed class CommentsSteps
    {
        [Then(@"I can see that user who left a comment with '(.*)' text is '(.*)'")]
        public void ThenICanSeeThatEmailOfUserWhoLeftACommentWithTextInCommentSBodyIs(string text, string email)
        {
            var response = ScenarioContext.Current.Get<HttpResponseMessage>(SharedSteps.CURRENT_GET_RESPONSE);
            List<Comment> commentsResponse = Deserializer.GetDeserializedObject<List<Comment>>(response.Content.ReadAsStringAsync().Result);

            var usersEmails = commentsResponse.Where(c => c.body.Contains(text) && c.email.Equals(email)).Select(u => u.email).ToList();

            Assert.IsNotNull(usersEmails.FirstOrDefault(), $"User with '{email}' email didn`t leave comment with '{text}' text!");
        }
    }
}
