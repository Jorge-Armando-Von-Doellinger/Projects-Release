// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Protos/auth.proto
// </auto-generated>
#pragma warning disable 0414, 1591, 8981, 0612
#region Designer generated code

using grpc = global::Grpc.Core;

namespace OmniSphere.Authentication.Grpc {
  public static partial class AuthService
  {
    static readonly string __ServiceName = "AuthService";

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static void __Helper_SerializeMessage(global::Google.Protobuf.IMessage message, grpc::SerializationContext context)
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (message is global::Google.Protobuf.IBufferMessage)
      {
        context.SetPayloadLength(message.CalculateSize());
        global::Google.Protobuf.MessageExtensions.WriteTo(message, context.GetBufferWriter());
        context.Complete();
        return;
      }
      #endif
      context.Complete(global::Google.Protobuf.MessageExtensions.ToByteArray(message));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static class __Helper_MessageCache<T>
    {
      public static readonly bool IsBufferMessage = global::System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(global::Google.Protobuf.IBufferMessage)).IsAssignableFrom(typeof(T));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static T __Helper_DeserializeMessage<T>(grpc::DeserializationContext context, global::Google.Protobuf.MessageParser<T> parser) where T : global::Google.Protobuf.IMessage<T>
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (__Helper_MessageCache<T>.IsBufferMessage)
      {
        return parser.ParseFrom(context.PayloadAsReadOnlySequence());
      }
      #endif
      return parser.ParseFrom(context.PayloadAsNewBuffer());
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::OmniSphere.Authentication.Grpc.UserCredentials> __Marshaller_UserCredentials = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::OmniSphere.Authentication.Grpc.UserCredentials.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::OmniSphere.Authentication.Grpc.JwtToken> __Marshaller_JwtToken = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::OmniSphere.Authentication.Grpc.JwtToken.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::OmniSphere.Authentication.Grpc.UserId> __Marshaller_UserId = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::OmniSphere.Authentication.Grpc.UserId.Parser));

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::OmniSphere.Authentication.Grpc.UserCredentials, global::OmniSphere.Authentication.Grpc.JwtToken> __Method_GetJwtToken = new grpc::Method<global::OmniSphere.Authentication.Grpc.UserCredentials, global::OmniSphere.Authentication.Grpc.JwtToken>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetJwtToken",
        __Marshaller_UserCredentials,
        __Marshaller_JwtToken);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::OmniSphere.Authentication.Grpc.JwtToken, global::OmniSphere.Authentication.Grpc.UserId> __Method_ValidateToken = new grpc::Method<global::OmniSphere.Authentication.Grpc.JwtToken, global::OmniSphere.Authentication.Grpc.UserId>(
        grpc::MethodType.Unary,
        __ServiceName,
        "ValidateToken",
        __Marshaller_JwtToken,
        __Marshaller_UserId);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::OmniSphere.Authentication.Grpc.AuthReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of AuthService</summary>
    [grpc::BindServiceMethod(typeof(AuthService), "BindService")]
    public abstract partial class AuthServiceBase
    {
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::OmniSphere.Authentication.Grpc.JwtToken> GetJwtToken(global::OmniSphere.Authentication.Grpc.UserCredentials request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::OmniSphere.Authentication.Grpc.UserId> ValidateToken(global::OmniSphere.Authentication.Grpc.JwtToken request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public static grpc::ServerServiceDefinition BindService(AuthServiceBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_GetJwtToken, serviceImpl.GetJwtToken)
          .AddMethod(__Method_ValidateToken, serviceImpl.ValidateToken).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public static void BindService(grpc::ServiceBinderBase serviceBinder, AuthServiceBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_GetJwtToken, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::OmniSphere.Authentication.Grpc.UserCredentials, global::OmniSphere.Authentication.Grpc.JwtToken>(serviceImpl.GetJwtToken));
      serviceBinder.AddMethod(__Method_ValidateToken, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::OmniSphere.Authentication.Grpc.JwtToken, global::OmniSphere.Authentication.Grpc.UserId>(serviceImpl.ValidateToken));
    }

  }
}
#endregion
