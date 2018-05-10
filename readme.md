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

## Development Requirements

 - .NET 4.7
 - Visual Studio 2015 or 2017
