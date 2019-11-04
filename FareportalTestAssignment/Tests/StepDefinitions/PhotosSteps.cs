using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using FareportalTestAssignment.Responses;
using NUnit.Framework;
using RestClient.Core;
using TechTalk.SpecFlow;

namespace FareportalTestAssignment.Tests.StepDefinitions
{
    [Binding]
    public class PhotosSteps
    {
        readonly RestClient.Core.RestClient restClient = new RestClient.Core.RestClient();


        [Given(@"I have received id by '(.*)' email")]
        public void GivenIHaveReceivedIdByEmail(string email)
        {
            var response = ScenarioContext.Current.Get<HttpResponseMessage>(SharedSteps.CURRENT_GET_RESPONSE);
            List<User> usersResponse = Deserializer.GetDeserializedObject<List<User>>(response.Content.ReadAsStringAsync().Result);

            User user = usersResponse.FirstOrDefault(u => u.email.Equals(email));
            if (user != null)
            {
                ScenarioContext.Current[SharedSteps.CURRENT_USER_ID_1] = user.id;
            }
            else
            {
                throw new ArgumentException($"'{email}' user was not found");
            }
        }

        [Given(@"I have received photo with id '(.*)'")]
        public void GivenIHaveReceivedPhotoForUserWithId(int Id)
        {
            var response = ScenarioContext.Current.Get<HttpResponseMessage>(SharedSteps.CURRENT_GET_RESPONSE);
            List<Photo> photosResponse = Deserializer.GetDeserializedObject<List<Photo>>(response.Content.ReadAsStringAsync().Result);

            Photo photo = photosResponse.FirstOrDefault(p => p.id.Equals(Id));
            if (photo != null)
            {
                ScenarioContext.Current[SharedSteps.CURRENT_GET_RESPONSE] = photo;
            }
            else
            {
                throw new ArgumentException($"Photo with id '{Id}' was not found");
            }
        }

        [When(@"I get the image of the received Photo")]
        public void WhenIGetTheImageOfTheReceivedPhoto()
        {
            var photo = ScenarioContext.Current.Get<Photo>(SharedSteps.CURRENT_GET_RESPONSE);
            HttpResponseMessage image = restClient.Get(photo.url);
            ScenarioContext.Current[SharedSteps.CURRENT_GET_RESPONSE] = image;
        }

        [Then(@"I see that received image matches to expected one")]
        public void ThenISeeThatReceivedImageMatchesToExpectedOne()
        {
            HttpResponseMessage image = ScenarioContext.Current.Get<HttpResponseMessage>(SharedSteps.CURRENT_GET_RESPONSE);
            string pathToExpectedImage = Helpers.Helpers.GetPathToFileinAssemblyDirectory("d32776.png");

            byte[] expectedByteArray = File.ReadAllBytes(pathToExpectedImage);
            byte[] actualBytesArray = image.Content.ReadAsByteArrayAsync().Result;

            Assert.IsTrue(Helpers.Helpers.CompareByteArrays(expectedByteArray, actualBytesArray), "Images are not equals.");
        }


        [Then(@"I can see that photo with title '(.*)' text belongs to '(.*)'")]
        public void ThenICanSeeThatPhotoWithTitleTextBelongsTo(string text, string email)
        {
            int userId = ScenarioContext.Current.Get<int>(SharedSteps.CURRENT_USER_ID_1);
            var response = ScenarioContext.Current.Get<HttpResponseMessage>(SharedSteps.CURRENT_GET_RESPONSE);
            List<Photo> photosResponse = Deserializer.GetDeserializedObject<List<Photo>>(response.Content.ReadAsStringAsync().Result);

            List<Photo> photos = photosResponse.Where(u => u.title.Contains(text) && u.albumId.Equals(userId)).ToList();

            Assert.IsNotNull(photos.FirstOrDefault(), $"{email} doesn`t have a photo with '{text}' title!");
        }
    }
}
