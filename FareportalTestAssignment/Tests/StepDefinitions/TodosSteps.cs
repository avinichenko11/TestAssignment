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
    public class TodosSteps
    {
        [Given(@"I have received ids of '(.*)' and '(.*)'")]
        public void GivenIHaveReceivedIdsOfAnd(string firstName, string secondName)
        {
            var response = ScenarioContext.Current.Get<HttpResponseMessage>(SharedSteps.CURRENT_GET_RESPONSE);
            List<User> usersResponse = Deserializer.GetDeserializedObject<List<User>>(response.Content.ReadAsStringAsync().Result);

            User user_1 = usersResponse.FirstOrDefault(u => u.name.Equals(firstName));
            if (user_1 != null)
            {
                ScenarioContext.Current[SharedSteps.CURRENT_USER_ID_1] = user_1.id;
            }
            else
            {
                throw new ArgumentException($"'{firstName}' user isn't found");
            }

            User user_2 = usersResponse.FirstOrDefault(u => u.name.Equals(secondName));
            if (user_2 != null)
            {
                ScenarioContext.Current[SharedSteps.CURRENT_USER_ID_2] = user_2.id;
            }
            else
            {
                throw new ArgumentException($"'{secondName}' user isn't found");
            }
        }

        [Then(@"I can see that '(.*)' has more than '(.*)' compleated todos and more than '(.*)'")]
        public void ThenICanSeeThatHasMoreThanCompleatedTodos(string firstName, int count, string secondName)
        {
            int userId_1 = ScenarioContext.Current.Get<int>(SharedSteps.CURRENT_USER_ID_1);
            int userId_2 = ScenarioContext.Current.Get<int>(SharedSteps.CURRENT_USER_ID_2);

            var response = ScenarioContext.Current.Get<HttpResponseMessage>(SharedSteps.CURRENT_GET_RESPONSE);
            List<Todo> todoResponse = Deserializer.GetDeserializedObject<List<Todo>>(response.Content.ReadAsStringAsync().Result);

            var todos_1 = todoResponse.Where(t => t.completed.Equals(true) && t.userId.Equals(userId_1)).ToList();
            var todos_2 = todoResponse.Where(t => t.completed.Equals(true) && t.userId.Equals(userId_2)).ToList();

            Assert.IsTrue(todos_1.Count > count, $"{firstName} has less than {count} compleated todos");
            Assert.IsTrue(todos_1.Count > todos_2.Count, $"{firstName} has less than {secondName} compleated todos");
        }
    }
}
