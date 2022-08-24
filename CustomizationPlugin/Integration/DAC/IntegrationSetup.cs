using PX.Data;
using PX.Objects.CS;
using PX.Objects.SO;
using System;

namespace Integration
{
    [Serializable]
    [PXCacheName("Integration Setup")]
    public class IntegrationSetup : IBqlTable
    {
        #region IntegrationID
        [PXDBString(250, IsKey = true, InputMask = "")]
        [PXUIField(DisplayName = "Integration ID")]
        [PXSelector(typeof(Search<IntegrationSetup.integrationID>), typeof(IntegrationSetup.integrationID))]
        public virtual string IntegrationID { get; set; }
        public abstract class integrationID : PX.Data.BQL.BqlString.Field<integrationID> { }
        #endregion

        #region OrderType
        [PXDBString(250, IsFixed = true, InputMask = ">aa")]
        [PXDefault]
        [PXSelector(typeof(Search2<SOOrderType.orderType, InnerJoin<SOOrderTypeOperation, On<SOOrderTypeOperation.orderType, Equal<SOOrderType.orderType>, And<SOOrderTypeOperation.operation, Equal<SOOrderType.defaultOperation>>>>,
            Where<SOOrderType.requireShipping, Equal<boolFalse>, Or<FeatureInstalled<FeaturesSet.inventory>>>>))]
        [PXUIField(DisplayName = "Order Type", Required = true)]
        public virtual string OrderType { get; set; }
        public abstract class orderType : PX.Data.BQL.BqlString.Field<orderType> { }
        #endregion
    }
}
