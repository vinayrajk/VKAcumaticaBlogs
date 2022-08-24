using System;
using PX.Data;
using Customization;
using PX.Objects.SO;
using PX.Objects.CS;

namespace Integration
{
    //Customization plugin is used to execute custom actions after customization project was published 
    public class MyPlugIn : CustomizationPlugin
    {
        //This method executed right after website files were updated, but before website was restarted
        //Method invoked on each cluster node in cluster environment
        //Method invoked only if runtimecompilation is enabled
        //Do not access custom code published to bin folder, it may not be loaded yet
        public override void OnPublished()
        {
            WriteLog("OnPublished Event");
        }

        //This method executed after customization was published and website was restarted.
        public override void UpdateDatabase()
        {
            #region Numbering sequence

            NumberingMaint graph = PXGraph.CreateInstance<NumberingMaint>();
            string newNumbering = EBConstants.serviceRepairOrderType;
            Numbering numbering1 = PXSelect<Numbering, Where<Numbering.numberingID, Equal<Required<Numbering.numberingID>>>>.Select(graph, "EBOrders");
            if (numbering1 == null)
            {
                var numbering = graph.Header.Insert();

                numbering.NumberingID =EBConstants.NumberingID;

                numbering.Descr =EBConstants.Descr;

                numbering.NewSymbol = EBConstants.NewSymbol;

                var soNumbering = graph.Sequence.Insert();

                soNumbering.StartNbr = EBConstants.StartNbr;

                soNumbering.EndNbr = EBConstants.EndNbr;

                soNumbering.StartDate = Convert.ToDateTime(Convert.ToDateTime(graph.Accessinfo.BusinessDate).ToShortDateString());

                soNumbering.LastNbr = EBConstants.LastNbr;

                soNumbering.WarnNbr =EBConstants.WarnNbr;

                soNumbering.NbrStep =EBConstants.NbrStep;

                graph.Persist();
                WriteLog(string.Format("{0} Numbering sequence has been added", newNumbering));
            }
            else
                WriteLog(string.Format("{0} Numbering sequence already created", newNumbering));

            #endregion

            #region OrderType

            SOOrderTypeMaint otgraph = PXGraph.CreateInstance<SOOrderTypeMaint>();
            string newOrderTypeName = EBConstants.serviceRepairOrderType;
           SOOrderType sOOrderType = PXSelect<SOOrderType, Where<SOOrderType.orderType, Equal<Required<SOOrderType.orderType>>>>.Select(otgraph, "EB");  
            if (sOOrderType == null)
            {
                //Assigning default values to the OrderType Obeject
                //Here you can assign the required values that you would like to create the ordertype based on the requirement during the package publish process
                var soSRTypeGeneral = otgraph.soordertype.Insert();
                soSRTypeGeneral.OrderType = EBConstants.OrderType;
                soSRTypeGeneral.Descr = EBConstants.Descr;
                soSRTypeGeneral.Template =EBConstants.Template;
                soSRTypeGeneral.Active = EBConstants.Active;
                soSRTypeGeneral.OrderNumberingID = EBConstants.OrderNumberingID;
                soSRTypeGeneral.DaysToKeep =EBConstants.DaysToKeep;
                soSRTypeGeneral.CalculateFreight = EBConstants.CalculateFreight;
                soSRTypeGeneral.SupportsApproval = EBConstants.SupportsApproval; 
                soSRTypeGeneral.InvoiceNumberingID = EBConstants.InvoiceNumberingID;
                soSRTypeGeneral.SalesAcctDefault = EBConstants.SalesAcctDefault;
                soSRTypeGeneral.SalesSubMask = EBConstants.SalesSubMask;
                soSRTypeGeneral.FreightAcctID = EBConstants.FreightAcctID;
                soSRTypeGeneral.FreightAcctDefault =EBConstants.FreightAcctDefault;
                soSRTypeGeneral.FreightSubID = EBConstants.FreightSubID;
                soSRTypeGeneral.FreightSubMask = EBConstants.FreightSubMask;
                soSRTypeGeneral.DiscountAcctID = EBConstants.DiscountAcctID;
                soSRTypeGeneral.DiscAcctDefault =EBConstants.DiscAcctDefault;
                soSRTypeGeneral.DiscountSubID = EBConstants.DiscountSubID;
                soSRTypeGeneral.DiscSubMask =EBConstants.DiscSubMask;
                soSRTypeGeneral.Behavior = EBConstants.Behavior;
                soSRTypeGeneral.DefaultOperation = EBConstants.DefaultOperation;
                soSRTypeGeneral.ARDocType = EBConstants.ARDocType;
                soSRTypeGeneral.AllowQuickProcess = EBConstants.AllowQuickProcess;
                soSRTypeGeneral.RequireShipping = EBConstants.RequireShipping;
                soSRTypeGeneral.INDocType = EBConstants.INDocType;
                var soSRTypeTemplate = otgraph.operations.Insert();
                soSRTypeTemplate.OrderType = EBConstants.OrderType;
                soSRTypeTemplate.Active = EBConstants.Active;
                soSRTypeTemplate.InvtMult = EBConstants.InvtMult;
                soSRTypeTemplate.Operation = EBConstants.Operation;
                soSRTypeTemplate.INDocType = EBConstants.INDocType;
                soSRTypeTemplate.OrderPlanType = EBConstants.OrderPlanType;
                soSRTypeTemplate.ShipmentPlanType = EBConstants.ShipmentPlanType;
                otgraph.operations.Cache.Update(soSRTypeTemplate);
                var soSRTypeQuickParam = otgraph.quickProcessPreset.Insert();
                soSRTypeQuickParam.OrderType = EBConstants.OrderType;
                soSRTypeQuickParam.UpdateIN = EBConstants.UpdateIN;
                soSRTypeQuickParam.CreateShipment = EBConstants.CreateShipment;
                soSRTypeQuickParam.PrepareInvoice = EBConstants.PrepareInvoice;
                soSRTypeQuickParam.ConfirmShipment = EBConstants.ConfirmShipment;
                otgraph.quickProcessPreset.Cache.Update(soSRTypeQuickParam);
                otgraph.Persist();
                WriteLog(string.Format("{0} Order type has been added", newOrderTypeName));
            }
            else
                WriteLog(string.Format("{0} Order type already created", newOrderTypeName));
            #endregion

            #region  Integration Setup

            IntegrationSetupMaint setupgraph = PXGraph.CreateInstance<IntegrationSetupMaint>();
            string integrationsetupgraph = EBConstants.serviceRepairOrderType;
            IntegrationSetup integrationSetup = PXSelect<IntegrationSetup, Where<IntegrationSetup.integrationID, Equal<Required<IntegrationSetup.integrationID>>>>.Select(setupgraph, "EBay Orders"); 
            if (integrationSetup == null)
            {
                var sointegration = setupgraph.IntegrationSetup.Insert();

                sointegration.IntegrationID = EBConstants.IntegrationID;
                sointegration.OrderType = EBConstants.OrderType;
                setupgraph.Persist();
                WriteLog(string.Format("{0} eBay Integration ID has been added", integrationsetupgraph));
            }
            else
                WriteLog(string.Format("{0} eBay Integration ID already created", integrationsetupgraph));

            #endregion

        }
    }
}








