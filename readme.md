# Course Outcomes WCF Service

## Overview
This service provides an endpoint for pulling learning outcomes for a given course. It is currently only used by Class Schedule.

## Usage

### Example service side configuration

```
<system.serviceModel>
	<services>
	  <service name="CourseOutcomesWCF.CourseOutcomes" behaviorConfiguration="DefaultBehavior">
	    <endpoint address="" binding="wsHttpBinding" contract="CourseOutcomesWCF.IService1" bindingConfiguration="wsHttps" />
	    <endpoint address="mex" binding="mexHttpsBinding" contract="IMetadataExchange" />
	  </service>
	</services>
	<behaviors>
	  <serviceBehaviors>
	    <behavior name="DefaultBehavior">
	      <serviceThrottling maxConcurrentCalls="50" maxConcurrentSessions="50" />
	      <serviceMetadata httpsGetEnabled="true" />
	      <serviceDebug includeExceptionDetailInFaults="false" httpsHelpPageEnabled="false" />
	    </behavior>
	  </serviceBehaviors>
	</behaviors>
	<bindings>
	  <wsHttpBinding>
	    <binding name="wsHttps" maxReceivedMessageSize="50000000" maxBufferPoolSize="50000000">
	      <readerQuotas maxDepth="500000000" maxArrayLength="500000000" maxBytesPerRead="500000000" maxNameTableCharCount="500000000" maxStringContentLength="500000000" />
	      <security mode="Transport">
	        <transport clientCredentialType="None" />
	      </security>
	    </binding>
	  </wsHttpBinding>
	</bindings>
	<serviceHostingEnvironment multipleSiteBindingsEnabled="true" />
</system.serviceModel>
```

### Example client side configuration

```
<system.serviceModel>
	<bindings>
	  <wsHttpBinding>
	    <binding name="WSHttpBinding_CourseOutcomes" closeTimeout="00:01:00" openTimeout="00:01:00" receiveTimeout="00:10:00" sendTimeout="00:01:00" bypassProxyOnLocal="false" transactionFlow="false" hostNameComparisonMode="StrongWildcard" maxBufferPoolSize="524288" maxReceivedMessageSize="65536" messageEncoding="Text" textEncoding="utf-8" useDefaultWebProxy="true" allowCookies="false">
	      <readerQuotas maxDepth="32" maxStringContentLength="8192" maxArrayLength="16384" maxBytesPerRead="4096" maxNameTableCharCount="16384" />
	      <reliableSession ordered="true" inactivityTimeout="00:10:00" enabled="false" />
	      <security mode="Transport">
	        <transport clientCredentialType="None" proxyCredentialType="None" realm="" />
	        <message clientCredentialType="Windows" negotiateServiceCredential="true" />
	      </security>
	    </binding>
	  </wsHttpBinding>
	</bindings>
	<client>
	  <endpoint address="https://www.bellevuecollege.edu/ws/courseoutcomes/Service1.svc" binding="wsHttpBinding" bindingConfiguration="WSHttpBinding_CourseOutcomes" contract="IService1" name="WSHttpBinding_CourseOutcomes" />
	</client>
</system.serviceModel>
```

### Endpoint

```GetCourseOutcome(string courseId)```

Get learning outcomes for a course by course id.

Example usage:

```csharp
Service1Client client = new ServiceClient();
string rawCourseOutcomes = client.GetCourseOutcome("PHIL 247");
```

## Development 

### Requirements

 - .NET 4.7
 - Visual Studio 2015 or 2017

### How to update client code

If you change the CourseOutcomes web service - add a function, change parameters for an existing function, etc - you will need to generate updated client code for use in applications utilizing the service. Currently, the only BC application using the CourseOutcomes web service is Class Schedule. Class Schedule includes the client code directly in [Common/CourseOutcomes.cs](https://github.com/BellevueCollege/ClassSchedule/blob/dev/ClassSchedule.Web/Common/CourseOutcomes.cs).

To generate the client code, you will need to use `SvcUtil.exe`, included with .NET tools (example location below, but your version may be elsewhere depending on your setup).

```
C:\Program Files (x86)\Microsoft SDKs\Windows\v10.0A\bin\NETFX 4.7 Tools>SvcUtil.exe /directory:C:\ /language:csharp /out:CourseOutcomes.cs https://wherever.bellevuecollege.edu/courseoutcomes/Service1.svc?wsdl
```

The URL in the example above should be the link to where the updated CourseOutcomes WCF service is being served.  It can be a local, test, or production environment. The example above will generate new client code in C#, save it in file `CourseOutcomes.cs`, and put it in the C:\ directory.  You can then copy that code to Class Schedule.

 * [More info about SvcUtil.exe](https://docs.microsoft.com/en-us/dotnet/framework/wcf/servicemodel-metadata-utility-tool-svcutil-exe)

