using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class OffsetGrab : XRGrabInteractable
{
   private Vector3 interactionPos = Vector3.zero;
   private Quaternion interactionRot = Quaternion.identity;

   protected override void OnSelectEntered(XRBaseInteractor interactor)
   {
      base.OnSelectEntered(interactor);
      StoreInteractor(interactor);
      MatchAttachmentPoints(interactor);
   }

   private void StoreInteractor(XRBaseInteractor interactor)
   {
      interactionPos = interactor.attachTransform.localPosition;
      interactionRot = interactor.attachTransform.localRotation;
   }

   private void MatchAttachmentPoints(XRBaseInteractor interactor)
   {
      bool isAttach = attachTransform != null;

      interactor.attachTransform.position = isAttach ? attachTransform.position : transform.position;
      interactor.attachTransform.rotation = isAttach ? attachTransform.rotation : transform.rotation;
   }

   protected override void OnSelectExited(XRBaseInteractor interactor)
   {
      base.OnSelectExited(interactor);
      ResetAttachmentPoints(interactor);
      ClearInteractor(interactor);
   }

   private void ResetAttachmentPoints(XRBaseInteractor interactor)
   {
      interactor.attachTransform.localPosition = interactionPos;
      interactor.attachTransform.localRotation = interactionRot;
   }

   private void ClearInteractor(XRBaseInteractor interactor)
   {
      interactionPos = Vector3.zero;
      interactionRot = Quaternion.identity;
   }
   
   
}
