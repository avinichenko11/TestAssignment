using System;
using System.Collections.Generic;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace FareportalTestAssignment.Tests.StepDefinitions
{
    [Binding]
    public class SharedSteps
    {
        public const string CURRENT_URL = "CURRENT_URL";
        public const string CURRENT_USER_ID_1 = "CURRENT_USER_ID_1";
        public const string CURRENT_USER_ID_2 = "CURRENT_USER_ID_2";
        public const string CURRENT_GET_RESPONSE = "CURRENT_GET_RESPONSE";
        public const string OBJECT_SEND_DATA_PREPARED = "OBJECT_SEND_DATA_PREPARED";
        public const string CURRENT_POST_RESPONSE_ID = "CURRENT_POST_RESPONSE_ID";
        public const string CURRENT_PUT_RESULT_CODE = "CURRENT_PUT_RESULT_CODE";
        public const string CURRENT_GET_RESULT_CODE = "CURRENT_GET_RESULT_CODE";
        public const string CURRENT_POST_RESULT_CODE = "CURRENT_POST_RESULT_CODE";
        public const string CURRENT_DELETE_RESULT_CODE = "CURRENT_DELETET_RESULT_CODE";


        [Then(@"Response result code for (GetNew|Get|Delete|Post|Put) operation is '(.*)'")]
        public void ThenResponseResultCodeForOperationsIs(string operation, int responsecode)
        {
            Dictionary<string, string> statusToTag = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                {"put", CURRENT_PUT_RESULT_CODE},
                {"post", CURRENT_POST_RESULT_CODE},
                {"delete", CURRENT_DELETE_RESULT_CODE},
                {"get", CURRENT_GET_RESULT_CODE},
            };

            int storeResultCode = int.Parse(ScenarioContext.Current[statusToTag[operation]].ToString());

            Assert.AreEqual(responsecode, storeResultCode, string.Format("Result code returned for operatoin {0} is: {1} ", operation, storeResultCode));
        }
    }
}
