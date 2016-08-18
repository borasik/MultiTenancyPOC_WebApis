using DH.PaymentAllocationService.Properties;
using System.Collections.Generic;
using InRule.Runtime;
using System.Xml;
using System.Xml.Linq;
using System.ServiceModel;
using DH.PaymentAllocationObjects;
using System.Xml.Serialization;
using System.IO;

namespace DH.PaymentAllocationService
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerSession, Namespace = Constants.Namespace)]
    public class InRuleService : IInRuleService
    {
        public InRuleResponse PaymentAllocationRules(InRuleRequest request)
        {
            ServiceData myServiceData = new ServiceData();
            var response = new InRuleResponse();
            var ruleAppNotifications = string.Empty;
            try
            {
                if (Settings.Default.IsHardCoded)
                {
                    XmlSerializer reader = new XmlSerializer(typeof(InRuleResponse));
                    using (StreamReader file = new StreamReader(Settings.Default.RuleAppDirectory + "InRuleResponse.xml"))
                    {
                        response = (InRuleResponse)reader.Deserialize(file);
                        file.Close();
                    }
                }
                else
                {
                    var ruleApp = new FileSystemRuleApplicationReference(Settings.Default.RuleAppDirectory + Settings.Default.RuleAppObject);
                    using (var session = new RuleSession(ruleApp))
                    {
                        var objectEntityState = Util.DeserializeFromXml<PaymentAllocationRoot>("<" + Settings.Default.TopEntityName + ">" + request.RuleDataXML + "</" + Settings.Default.TopEntityName + ">");
                        objectEntityState.RuleEvaluationDate = request.RuleInfo.RuleEvaluationDate;
                        var rootEntity = session.CreateEntity(Settings.Default.TopEntityName, objectEntityState);
                    
                        if (Settings.Default.UseLog)
                            session.Settings.LogOptions = EngineLogOptions.Execution | EngineLogOptions.RuleTrace;
                        else
                            session.Settings.LogOptions = EngineLogOptions.None;

                        RuleExecutionLog executionLog = rootEntity.ExecuteRuleSet(request.RuleInfo.RuleName, Settings.Default.PerformPreValidation, Settings.Default.PerformPostValidation);

                        if (Settings.Default.UseLog)
                        {
                            string logMessages = string.Empty;
                            foreach (TextFeedbackLogMessage logMessage in executionLog.TextFeedbackMessages)
                            {
                                logMessages += logMessage.Description + "\r\n";
                            }

                            if (executionLog.HasNotifications)
                            {
                                foreach (var notification in session.GetNotifications())
                                {
                                    ruleAppNotifications += notification.Message + "\r\n";
                                }
                            }
                        }

                        response.RuleInfo = request.RuleInfo;
                        response.RuleInfo.ErrorDetails = objectEntityState.ErrorDetails;
                        response.RuleInfo.Status = objectEntityState.ErrorDetails.Count > 0? "Failure": "Success";
                        response.RuleResult = Util.GetRuleResultXml(objectEntityState.Payments);
                    }
                }
                Util.SerializeToXmlFile(response, Settings.Default.RuleAppDirectory + @"\InRuleResponse.xml");
            }
            catch (System.Exception ex)
            {
                myServiceData.Result = false;
                myServiceData.ErrorMessage = "PaymentAllocationRules call failed";
                myServiceData.ErrorDetails = ex.ToString();
                throw new FaultException<ServiceData>(myServiceData, ex.ToString());
            }
            return response;
        }
    }
}
