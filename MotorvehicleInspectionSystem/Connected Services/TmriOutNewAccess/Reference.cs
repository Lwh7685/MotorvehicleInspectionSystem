﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//
//     对此文件的更改可能导致不正确的行为，并在以下条件下丢失:
//     代码重新生成。
// </auto-generated>
//------------------------------------------------------------------------------

namespace TmriOutNewAccess
{
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="TmriOutNewAccess.TmriOutNewAccessSoap")]
    public interface TmriOutNewAccessSoap
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/HelloWorld", ReplyAction="*")]
        System.Threading.Tasks.Task<TmriOutNewAccess.HelloWorldResponse> HelloWorldAsync(TmriOutNewAccess.HelloWorldRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/queryObjectOutNew", ReplyAction="*")]
        System.Threading.Tasks.Task<TmriOutNewAccess.queryObjectOutNewResponse> queryObjectOutNewAsync(TmriOutNewAccess.queryObjectOutNewRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/writeObjectOutNew", ReplyAction="*")]
        System.Threading.Tasks.Task<TmriOutNewAccess.writeObjectOutNewResponse> writeObjectOutNewAsync(TmriOutNewAccess.writeObjectOutNewRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class HelloWorldRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="HelloWorld", Namespace="http://tempuri.org/", Order=0)]
        public TmriOutNewAccess.HelloWorldRequestBody Body;
        
        public HelloWorldRequest()
        {
        }
        
        public HelloWorldRequest(TmriOutNewAccess.HelloWorldRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute()]
    public partial class HelloWorldRequestBody
    {
        
        public HelloWorldRequestBody()
        {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class HelloWorldResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="HelloWorldResponse", Namespace="http://tempuri.org/", Order=0)]
        public TmriOutNewAccess.HelloWorldResponseBody Body;
        
        public HelloWorldResponse()
        {
        }
        
        public HelloWorldResponse(TmriOutNewAccess.HelloWorldResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class HelloWorldResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string HelloWorldResult;
        
        public HelloWorldResponseBody()
        {
        }
        
        public HelloWorldResponseBody(string HelloWorldResult)
        {
            this.HelloWorldResult = HelloWorldResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class queryObjectOutNewRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="queryObjectOutNew", Namespace="http://tempuri.org/", Order=0)]
        public TmriOutNewAccess.queryObjectOutNewRequestBody Body;
        
        public queryObjectOutNewRequest()
        {
        }
        
        public queryObjectOutNewRequest(TmriOutNewAccess.queryObjectOutNewRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class queryObjectOutNewRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string xtlb;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string jkxlh;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string jkid;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string cjsqbh;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public string dwjgdm;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=5)]
        public string dwmc;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=6)]
        public string yhbz;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=7)]
        public string yhxm;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=8)]
        public string zdbs;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=9)]
        public string QueryXmlDoc;
        
        public queryObjectOutNewRequestBody()
        {
        }
        
        public queryObjectOutNewRequestBody(string xtlb, string jkxlh, string jkid, string cjsqbh, string dwjgdm, string dwmc, string yhbz, string yhxm, string zdbs, string QueryXmlDoc)
        {
            this.xtlb = xtlb;
            this.jkxlh = jkxlh;
            this.jkid = jkid;
            this.cjsqbh = cjsqbh;
            this.dwjgdm = dwjgdm;
            this.dwmc = dwmc;
            this.yhbz = yhbz;
            this.yhxm = yhxm;
            this.zdbs = zdbs;
            this.QueryXmlDoc = QueryXmlDoc;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class queryObjectOutNewResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="queryObjectOutNewResponse", Namespace="http://tempuri.org/", Order=0)]
        public TmriOutNewAccess.queryObjectOutNewResponseBody Body;
        
        public queryObjectOutNewResponse()
        {
        }
        
        public queryObjectOutNewResponse(TmriOutNewAccess.queryObjectOutNewResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class queryObjectOutNewResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string queryObjectOutNewResult;
        
        public queryObjectOutNewResponseBody()
        {
        }
        
        public queryObjectOutNewResponseBody(string queryObjectOutNewResult)
        {
            this.queryObjectOutNewResult = queryObjectOutNewResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class writeObjectOutNewRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="writeObjectOutNew", Namespace="http://tempuri.org/", Order=0)]
        public TmriOutNewAccess.writeObjectOutNewRequestBody Body;
        
        public writeObjectOutNewRequest()
        {
        }
        
        public writeObjectOutNewRequest(TmriOutNewAccess.writeObjectOutNewRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class writeObjectOutNewRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string xtlb;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string jkxlh;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string jkid;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string cjsqbh;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public string dwjgdm;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=5)]
        public string dwmc;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=6)]
        public string yhbz;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=7)]
        public string yhxm;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=8)]
        public string zdbs;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=9)]
        public string WriteXmlDoc;
        
        public writeObjectOutNewRequestBody()
        {
        }
        
        public writeObjectOutNewRequestBody(string xtlb, string jkxlh, string jkid, string cjsqbh, string dwjgdm, string dwmc, string yhbz, string yhxm, string zdbs, string WriteXmlDoc)
        {
            this.xtlb = xtlb;
            this.jkxlh = jkxlh;
            this.jkid = jkid;
            this.cjsqbh = cjsqbh;
            this.dwjgdm = dwjgdm;
            this.dwmc = dwmc;
            this.yhbz = yhbz;
            this.yhxm = yhxm;
            this.zdbs = zdbs;
            this.WriteXmlDoc = WriteXmlDoc;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class writeObjectOutNewResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="writeObjectOutNewResponse", Namespace="http://tempuri.org/", Order=0)]
        public TmriOutNewAccess.writeObjectOutNewResponseBody Body;
        
        public writeObjectOutNewResponse()
        {
        }
        
        public writeObjectOutNewResponse(TmriOutNewAccess.writeObjectOutNewResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class writeObjectOutNewResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string writeObjectOutNewResult;
        
        public writeObjectOutNewResponseBody()
        {
        }
        
        public writeObjectOutNewResponseBody(string writeObjectOutNewResult)
        {
            this.writeObjectOutNewResult = writeObjectOutNewResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    public interface TmriOutNewAccessSoapChannel : TmriOutNewAccess.TmriOutNewAccessSoap, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    public partial class TmriOutNewAccessSoapClient : System.ServiceModel.ClientBase<TmriOutNewAccess.TmriOutNewAccessSoap>, TmriOutNewAccess.TmriOutNewAccessSoap
    {
        
        /// <summary>
        /// 实现此分部方法，配置服务终结点。
        /// </summary>
        /// <param name="serviceEndpoint">要配置的终结点</param>
        /// <param name="clientCredentials">客户端凭据</param>
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public TmriOutNewAccessSoapClient(EndpointConfiguration endpointConfiguration) : 
                base(TmriOutNewAccessSoapClient.GetBindingForEndpoint(endpointConfiguration), TmriOutNewAccessSoapClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public TmriOutNewAccessSoapClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(TmriOutNewAccessSoapClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public TmriOutNewAccessSoapClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(TmriOutNewAccessSoapClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public TmriOutNewAccessSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<TmriOutNewAccess.HelloWorldResponse> TmriOutNewAccess.TmriOutNewAccessSoap.HelloWorldAsync(TmriOutNewAccess.HelloWorldRequest request)
        {
            return base.Channel.HelloWorldAsync(request);
        }
        
        public System.Threading.Tasks.Task<TmriOutNewAccess.HelloWorldResponse> HelloWorldAsync()
        {
            TmriOutNewAccess.HelloWorldRequest inValue = new TmriOutNewAccess.HelloWorldRequest();
            inValue.Body = new TmriOutNewAccess.HelloWorldRequestBody();
            return ((TmriOutNewAccess.TmriOutNewAccessSoap)(this)).HelloWorldAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<TmriOutNewAccess.queryObjectOutNewResponse> TmriOutNewAccess.TmriOutNewAccessSoap.queryObjectOutNewAsync(TmriOutNewAccess.queryObjectOutNewRequest request)
        {
            return base.Channel.queryObjectOutNewAsync(request);
        }
        
        public System.Threading.Tasks.Task<TmriOutNewAccess.queryObjectOutNewResponse> queryObjectOutNewAsync(string xtlb, string jkxlh, string jkid, string cjsqbh, string dwjgdm, string dwmc, string yhbz, string yhxm, string zdbs, string QueryXmlDoc)
        {
            TmriOutNewAccess.queryObjectOutNewRequest inValue = new TmriOutNewAccess.queryObjectOutNewRequest();
            inValue.Body = new TmriOutNewAccess.queryObjectOutNewRequestBody();
            inValue.Body.xtlb = xtlb;
            inValue.Body.jkxlh = jkxlh;
            inValue.Body.jkid = jkid;
            inValue.Body.cjsqbh = cjsqbh;
            inValue.Body.dwjgdm = dwjgdm;
            inValue.Body.dwmc = dwmc;
            inValue.Body.yhbz = yhbz;
            inValue.Body.yhxm = yhxm;
            inValue.Body.zdbs = zdbs;
            inValue.Body.QueryXmlDoc = QueryXmlDoc;
            return ((TmriOutNewAccess.TmriOutNewAccessSoap)(this)).queryObjectOutNewAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<TmriOutNewAccess.writeObjectOutNewResponse> TmriOutNewAccess.TmriOutNewAccessSoap.writeObjectOutNewAsync(TmriOutNewAccess.writeObjectOutNewRequest request)
        {
            return base.Channel.writeObjectOutNewAsync(request);
        }
        
        public System.Threading.Tasks.Task<TmriOutNewAccess.writeObjectOutNewResponse> writeObjectOutNewAsync(string xtlb, string jkxlh, string jkid, string cjsqbh, string dwjgdm, string dwmc, string yhbz, string yhxm, string zdbs, string WriteXmlDoc)
        {
            TmriOutNewAccess.writeObjectOutNewRequest inValue = new TmriOutNewAccess.writeObjectOutNewRequest();
            inValue.Body = new TmriOutNewAccess.writeObjectOutNewRequestBody();
            inValue.Body.xtlb = xtlb;
            inValue.Body.jkxlh = jkxlh;
            inValue.Body.jkid = jkid;
            inValue.Body.cjsqbh = cjsqbh;
            inValue.Body.dwjgdm = dwjgdm;
            inValue.Body.dwmc = dwmc;
            inValue.Body.yhbz = yhbz;
            inValue.Body.yhxm = yhxm;
            inValue.Body.zdbs = zdbs;
            inValue.Body.WriteXmlDoc = WriteXmlDoc;
            return ((TmriOutNewAccess.TmriOutNewAccessSoap)(this)).writeObjectOutNewAsync(inValue);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.TmriOutNewAccessSoap))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                return result;
            }
            if ((endpointConfiguration == EndpointConfiguration.TmriOutNewAccessSoap12))
            {
                System.ServiceModel.Channels.CustomBinding result = new System.ServiceModel.Channels.CustomBinding();
                System.ServiceModel.Channels.TextMessageEncodingBindingElement textBindingElement = new System.ServiceModel.Channels.TextMessageEncodingBindingElement();
                textBindingElement.MessageVersion = System.ServiceModel.Channels.MessageVersion.CreateVersion(System.ServiceModel.EnvelopeVersion.Soap12, System.ServiceModel.Channels.AddressingVersion.None);
                result.Elements.Add(textBindingElement);
                System.ServiceModel.Channels.HttpTransportBindingElement httpBindingElement = new System.ServiceModel.Channels.HttpTransportBindingElement();
                httpBindingElement.AllowCookies = true;
                httpBindingElement.MaxBufferSize = int.MaxValue;
                httpBindingElement.MaxReceivedMessageSize = int.MaxValue;
                result.Elements.Add(httpBindingElement);
                return result;
            }
            throw new System.InvalidOperationException(string.Format("找不到名称为“{0}”的终结点。", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.TmriOutNewAccessSoap))
            {
                return new System.ServiceModel.EndpointAddress("http://localhost:8072/TmriOutNewAccess.asmx");
            }
            if ((endpointConfiguration == EndpointConfiguration.TmriOutNewAccessSoap12))
            {
                return new System.ServiceModel.EndpointAddress("http://localhost:8072/TmriOutNewAccess.asmx");
            }
            throw new System.InvalidOperationException(string.Format("找不到名称为“{0}”的终结点。", endpointConfiguration));
        }
        
        public enum EndpointConfiguration
        {
            
            TmriOutNewAccessSoap,
            
            TmriOutNewAccessSoap12,
        }
    }
}