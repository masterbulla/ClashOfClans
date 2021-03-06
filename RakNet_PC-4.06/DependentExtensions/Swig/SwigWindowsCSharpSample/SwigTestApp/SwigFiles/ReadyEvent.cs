/* ----------------------------------------------------------------------------
 * This file was automatically generated by SWIG (http://www.swig.org).
 * Version 2.0.9
 *
 * Do not make changes to this file unless you know what you are doing--modify
 * the SWIG interface file instead.
 * ----------------------------------------------------------------------------- */

namespace RakNet {

using System;
using System.Runtime.InteropServices;

public class ReadyEvent : PluginInterface2 {
  private HandleRef swigCPtr;

  internal ReadyEvent(IntPtr cPtr, bool cMemoryOwn) : base(RakNetPINVOKE.ReadyEvent_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new HandleRef(this, cPtr);
  }

  internal static HandleRef getCPtr(ReadyEvent obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  ~ReadyEvent() {
    Dispose();
  }

  public override void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          RakNetPINVOKE.delete_ReadyEvent(swigCPtr);
        }
        swigCPtr = new HandleRef(null, IntPtr.Zero);
      }
      GC.SuppressFinalize(this);
      base.Dispose();
    }
  }

  public static ReadyEvent GetInstance() {
    IntPtr cPtr = RakNetPINVOKE.ReadyEvent_GetInstance();
    ReadyEvent ret = (cPtr == IntPtr.Zero) ? null : new ReadyEvent(cPtr, false);
    return ret;
  }

  public static void DestroyInstance(ReadyEvent i) {
    RakNetPINVOKE.ReadyEvent_DestroyInstance(ReadyEvent.getCPtr(i));
  }

  public ReadyEvent() : this(RakNetPINVOKE.new_ReadyEvent(), true) {
  }

  public bool SetEvent(int eventId, bool isReady) {
    bool ret = RakNetPINVOKE.ReadyEvent_SetEvent(swigCPtr, eventId, isReady);
    return ret;
  }

  public void ForceCompletion(int eventId) {
    RakNetPINVOKE.ReadyEvent_ForceCompletion(swigCPtr, eventId);
  }

  public bool DeleteEvent(int eventId) {
    bool ret = RakNetPINVOKE.ReadyEvent_DeleteEvent(swigCPtr, eventId);
    return ret;
  }

  public bool IsEventSet(int eventId) {
    bool ret = RakNetPINVOKE.ReadyEvent_IsEventSet(swigCPtr, eventId);
    return ret;
  }

  public bool IsEventCompletionProcessing(int eventId) {
    bool ret = RakNetPINVOKE.ReadyEvent_IsEventCompletionProcessing(swigCPtr, eventId);
    return ret;
  }

  public bool IsEventCompleted(int eventId) {
    bool ret = RakNetPINVOKE.ReadyEvent_IsEventCompleted(swigCPtr, eventId);
    return ret;
  }

  public bool HasEvent(int eventId) {
    bool ret = RakNetPINVOKE.ReadyEvent_HasEvent(swigCPtr, eventId);
    return ret;
  }

  public uint GetEventListSize() {
    uint ret = RakNetPINVOKE.ReadyEvent_GetEventListSize(swigCPtr);
    return ret;
  }

  public int GetEventAtIndex(uint index) {
    int ret = RakNetPINVOKE.ReadyEvent_GetEventAtIndex(swigCPtr, index);
    return ret;
  }

  public bool AddToWaitList(int eventId, SystemAddress address) {
    bool ret = RakNetPINVOKE.ReadyEvent_AddToWaitList(swigCPtr, eventId, SystemAddress.getCPtr(address));
    if (RakNetPINVOKE.SWIGPendingException.Pending) throw RakNetPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool RemoveFromWaitList(int eventId, SystemAddress address) {
    bool ret = RakNetPINVOKE.ReadyEvent_RemoveFromWaitList(swigCPtr, eventId, SystemAddress.getCPtr(address));
    if (RakNetPINVOKE.SWIGPendingException.Pending) throw RakNetPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool IsInWaitList(int eventId, SystemAddress address) {
    bool ret = RakNetPINVOKE.ReadyEvent_IsInWaitList(swigCPtr, eventId, SystemAddress.getCPtr(address));
    if (RakNetPINVOKE.SWIGPendingException.Pending) throw RakNetPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public uint GetRemoteWaitListSize(int eventId) {
    uint ret = RakNetPINVOKE.ReadyEvent_GetRemoteWaitListSize(swigCPtr, eventId);
    return ret;
  }

  public SystemAddress GetFromWaitListAtIndex(int eventId, uint index) {
    SystemAddress ret = new SystemAddress(RakNetPINVOKE.ReadyEvent_GetFromWaitListAtIndex(swigCPtr, eventId, index), true);
    return ret;
  }

  public ReadyEventSystemStatus GetReadyStatus(int eventId, SystemAddress address) {
    ReadyEventSystemStatus ret = (ReadyEventSystemStatus)RakNetPINVOKE.ReadyEvent_GetReadyStatus(swigCPtr, eventId, SystemAddress.getCPtr(address));
    if (RakNetPINVOKE.SWIGPendingException.Pending) throw RakNetPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void SetSendChannel(byte newChannel) {
    RakNetPINVOKE.ReadyEvent_SetSendChannel(swigCPtr, newChannel);
  }

}

}
