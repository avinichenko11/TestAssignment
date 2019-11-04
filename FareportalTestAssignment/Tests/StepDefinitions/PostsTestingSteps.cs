using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using FareportalTestAssignment.DataGenerators;
using FareportalTestAssignment.Responses;
using NUnit.Framework;
using RestClient.Core;
using TechTalk.SpecFlow;

namespace FareportalTestAssignment.Tests.StepDefinitions
{
    [Binding]
    public class PostsTestingSteps
    {
        readonly RestClient.Core.RestClient restClient = new RestClient.Core.RestClient();


        [Given(@"I have generated object to Post")]
        public void GivenIHaveGeneratedObjectToPost()
        {
            var objToPost = NewPost.GenerateObject();
            ScenarioContext.Current[SharedSteps.OBJECT_SEND_DATA_PREPARED] = objToPost;
        }

        [Given(@"I have new post for '(.*)'")]
        public void GivenIHaveNewPostForEditing(string test)
        {
            GivenIHaveGeneratedObjectToPost();
            WhenICallPostForWithGeneratedData();
        }

        [Given(@"I have generated object to Put")]
        public void GivenIHaveGeneratedObjectToPut()
        {
            var postId = ScenarioContext.Current.Get<int>(SharedSteps.CURRENT_POST_RESPONSE_ID);
            var objToPut = EditedPost.GenerateObject(postId);
            ScenarioContext.Current[SharedSteps.OBJECT_SEND_DATA_PREPARED] = objToPut;
        }

        [Given(@"I have received id of '(.*)'")]
        public void GivenIHaveReceivedIdOf(string name)
        {
            var response = ScenarioContext.Current.Get<HttpResponseMessage>(SharedSteps.CURRENT_GET_RESPONSE);
            List<User> usersResponse = Deserializer.GetDeserializedObject<List<User>>(response.Content.ReadAsStringAsync().Result);

            User user = usersResponse.FirstOrDefault(u => u.name.Equals(name));
            if (user != null)
            {
                ScenarioContext.Current[SharedSteps.CURRENT_USER_ID_1] = user.id;
            }
            else
            {
                throw new ArgumentException($"'{name}' user isn't found");
            }
        }

        [When(@"I call Post for '/posts' with generated data")]
        public void WhenICallPostForWithGeneratedData()
        {
            string url = ScenarioContext.Current.Get<string>(SharedSteps.CURRENT_URL);
            var objToPost = ScenarioContext.Current.Get<NewPost>(SharedSteps.OBJECT_SEND_DATA_PREPARED);
            var response = restClient.Post($"{url}/posts", objToPost);
            ScenarioContext.Current[SharedSteps.CURRENT_POST_RESULT_CODE] = (int) response.StatusCode;
            ScenarioContext.Current[SharedSteps.CURRENT_POST_RESPONSE_ID] = restClient.GetPostResponseId(response);
        }

        [Given(@"I have called Get for '(.*)' endpoint")]
        [When(@"I call Get for '(.*)' endpoint")]
        public void WhenICallGetForEndpoint(string endpoint)
        {
            string url = ScenarioContext.Current.Get<string>(SharedSteps.CURRENT_URL);
            var response = restClient.Get($"{url}{endpoint}");
            ScenarioContext.Current[SharedSteps.CURRENT_GET_RESULT_CODE] = (int)response.StatusCode;
            ScenarioContext.Current[SharedSteps.CURRENT_GET_RESPONSE] = response;
        }

        [When(@"I call Delete for '/posts'")]
        public void WhenICallDeleteFor()
        {
            var postId = ScenarioContext.Current.Get<int>(SharedSteps.CURRENT_POST_RESPONSE_ID);
            string url = ScenarioContext.Current.Get<string>(SharedSteps.CURRENT_URL);
            var response = restClient.Delete($"{url}/posts/{postId}");
            ScenarioContext.Current[SharedSteps.CURRENT_DELETE_RESULT_CODE] = (int)response.StatusCode;
        }

        [When(@"I call Put for '/posts' with generated data")]
        public void WhenICallPutForWithGeneratedData()
        {
            string url = ScenarioContext.Current.Get<string>(SharedSteps.CURRENT_URL);
            var objToPut = ScenarioContext.Current.Get<EditedPost>(SharedSteps.OBJECT_SEND_DATA_PREPARED);
            var response = restClient.Put($"{url}/posts", objToPut);
            ScenarioContext.Current[SharedSteps.CURRENT_PUT_RESULT_CODE] = (int)response.StatusCode;
        }

        [Then(@"I can see that generated post exists in result's list")]
        public void ThenICanSeeThatGeneratedPostExistsInResultSList()
        {
            var objToPost = ScenarioContext.Current.Get<NewPost>(SharedSteps.OBJECT_SEND_DATA_PREPARED);
            var responseId = ScenarioContext.Current.Get<int>(SharedSteps.CURRENT_POST_RESPONSE_ID);
            var response = ScenarioContext.Current.Get<HttpResponseMessage>(SharedSteps.CURRENT_GET_RESPONSE);
            List<Post> postsResponse = Deserializer.GetDeserializedObject<List<Post>>(response.Content.ReadAsStringAsync().Result);
            var newPost = postsResponse.FirstOrDefault(p => p.id.Equals(responseId));

            Assert.IsNotNull(newPost, "Post is not found in result's list");
            Assert.AreEqual(objToPost.userId, newPost.userId);
            Assert.AreEqual(objToPost.title, newPost.title);
            Assert.AreEqual(objToPost.body, newPost.body);
        }

        [Then(@"I can see that edited post exists in result's list")]
        public void ThenICanSeeThatEditedPostExistsInResultSList()
        {
            var objToPut = ScenarioContext.Current.Get<EditedPost>(SharedSteps.OBJECT_SEND_DATA_PREPARED);
            var responseId = ScenarioContext.Current.Get<int>(SharedSteps.CURRENT_POST_RESPONSE_ID);
            var response = ScenarioContext.Current.Get<HttpResponseMessage>(SharedSteps.CURRENT_GET_RESPONSE);
            List<Post> postsResponse = Deserializer.GetDeserializedObject<List<Post>>(response.Content.ReadAsStringAsync().Result);
            var editedPost = postsResponse.FirstOrDefault(p => p.id.Equals(responseId));

            Assert.IsNotNull(editedPost, "Edited post is not found in result's list");
            Assert.AreEqual(objToPut.userId, editedPost.userId);
            Assert.AreEqual(objToPut.title, editedPost.title);
            Assert.AreEqual(objToPut.body, editedPost.body);
        }

        [Then(@"I can see that new post doesn`t exist in result's list")]
        public void ThenICanSeeThatNewPostDoesnTExistInResultSList()
        {
            var responseId = ScenarioContext.Current.Get<int>(SharedSteps.CURRENT_POST_RESPONSE_ID);
            var response = ScenarioContext.Current.Get<HttpResponseMessage>(SharedSteps.CURRENT_GET_RESPONSE);
            List<Post> postsResponse = Deserializer.GetDeserializedObject<List<Post>>(response.Content.ReadAsStringAsync().Result);

            var newPost = postsResponse.FirstOrDefault(p => p.id.Equals(responseId));
            Assert.IsNull(newPost, "Post is present in result's list");
        }

        [Then(@"I can see that post with title '(.*)' belongs to '(.*)'")]
        public void ThenICanSeeThatPostWithTitleBelongTo(string text, string name)
        {
            int userId = ScenarioContext.Current.Get<int>(SharedSteps.CURRENT_USER_ID_1);
            var response = ScenarioContext.Current.Get<HttpResponseMessage>(SharedSteps.CURRENT_GET_RESPONSE);
            List<Post> commentsResponse = Deserializer.GetDeserializedObject<List<Post>>(response.Content.ReadAsStringAsync().Result);

            List<Post> posts = commentsResponse.Where(u => u.title.Contains(text) && u.userId.Equals(userId)).ToList();

            Assert.IsNotNull(posts.FirstOrDefault(), $"{name} didn`t post the post with '{text}' title!");
        }
    }
}