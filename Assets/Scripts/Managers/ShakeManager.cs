namespace RGSMS
{
    namespace Shake
    {
        using UnityEngine;
        using System.Collections;
        
        public enum AXIS
        {
            X_Y_Z = 0,
            X_Y,
            X_Z,
            Y_Z,
            X,
            Y,
            Z
        }
        
        public static class ShakeManager
        {
            // TO DO:
            // SMOOTH LERP
            // CRIAR SHAKES QUE DUREM ENQUANTO UMA BOOLEANA ESTIVER TRUE

            #region Position

            #region WORLD

            public static IEnumerator ShakePosition(this Transform transform, float intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakePosition(transform, intensity, duration, false, useFixedDeltaTime);
            }
            
            public static IEnumerator ShakePosition(this Transform transform, Vector3 intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakePosition(transform, intensity, duration, false, useFixedDeltaTime);
            }
            
            public static IEnumerator ShakePositionX(this Transform transform, float intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakePosition(transform, AXIS.X, intensity, duration, false, useFixedDeltaTime);
            }

            public static IEnumerator ShakePositionY(this Transform transform, float intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakePosition(transform, AXIS.Y, intensity, duration, false, useFixedDeltaTime);
            }

            public static IEnumerator ShakePositionZ(this Transform transform, float intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakePosition(transform, AXIS.Z, intensity, duration, false, useFixedDeltaTime);
            }

            public static IEnumerator ShakePositionXY(this Transform transform, float intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakePosition(transform, AXIS.X_Y, intensity, duration, false, useFixedDeltaTime);
            }

            public static IEnumerator ShakePositionXY(this Transform transform, Vector3 intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakePosition(transform, AXIS.X_Y, intensity, duration, false, useFixedDeltaTime);
            }

            public static IEnumerator ShakePositionXZ(this Transform transform, float intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakePosition(transform, AXIS.X_Z, intensity, duration, false, useFixedDeltaTime);
            }

            public static IEnumerator ShakePositionXZ(this Transform transform, Vector3 intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakePosition(transform, AXIS.X_Z, intensity, duration, false, useFixedDeltaTime);
            }

            public static IEnumerator ShakePositionYZ(this Transform transform, float intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakePosition(transform, AXIS.Y_Z, intensity, duration, false, useFixedDeltaTime);
            }

            public static IEnumerator ShakePositionYZ(this Transform transform, Vector3 intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakePosition(transform, AXIS.Y_Z, intensity, duration, false, useFixedDeltaTime);
            }

            #endregion

            #region LOCAL

            public static IEnumerator ShakeLocalPosition(this Transform transform, float intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakePosition(transform, intensity, duration, true, useFixedDeltaTime);
            }

            public static IEnumerator ShakeLocalPosition(this Transform transform, Vector3 intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakePosition(transform, intensity, duration, true, useFixedDeltaTime);
            }

            public static IEnumerator ShakeLocalPositionX(this Transform transform, float intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakePosition(transform, AXIS.X, intensity, duration, true, useFixedDeltaTime);
            }

            public static IEnumerator ShakeLocalPositionY(this Transform transform, float intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakePosition(transform, AXIS.Y, intensity, duration, true, useFixedDeltaTime);
            }

            public static IEnumerator ShakeLocalPositionZ(this Transform transform, float intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakePosition(transform, AXIS.Z, intensity, duration, true, useFixedDeltaTime);
            }

            public static IEnumerator ShakeLocalPositionXY(this Transform transform, float intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakePosition(transform, AXIS.X_Y, intensity, duration, true, useFixedDeltaTime);
            }

            public static IEnumerator ShakeLocalPositionXY(this Transform transform, Vector3 intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakePosition(transform, AXIS.X_Y, intensity, duration, true, useFixedDeltaTime);
            }

            public static IEnumerator ShakeLocalPositionXZ(this Transform transform, float intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakePosition(transform, AXIS.X_Z, intensity, duration, true, useFixedDeltaTime);
            }

            public static IEnumerator ShakeLocalPositionXZ(this Transform transform, Vector3 intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakePosition(transform, AXIS.X_Z, intensity, duration, true, useFixedDeltaTime);
            }

            public static IEnumerator ShakeLocalPositionYZ(this Transform transform, float intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakePosition(transform, AXIS.Y_Z, intensity, duration, true, useFixedDeltaTime);
            }

            public static IEnumerator ShakeLocalPositionYZ(this Transform transform, Vector3 intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakePosition(transform, AXIS.Y_Z, intensity, duration, true, useFixedDeltaTime);
            }

            #endregion
            
            #region PRIVATES

            private static IEnumerator ShakePosition (Transform transform, float intensity, float duration, bool isLocal, bool useFixedDeltaTime)
            {
                float time = 0.0f;
                Vector3 startPosition = isLocal ? transform.localPosition : transform.position;

                while (time < duration)
                {
                    time += useFixedDeltaTime ? Time.fixedDeltaTime : Time.deltaTime;

                    if (isLocal)
                    {
                        transform.localPosition = startPosition + (Random.insideUnitSphere * intensity);
                    }
                    else
                    {
                        transform.position = startPosition + (Random.insideUnitSphere * intensity);
                    }

                    yield return null;
                }

                if (isLocal)
                {
                    transform.localPosition = startPosition;
                }
                else
                {
                    transform.position = startPosition;
                }
            }

            private static IEnumerator ShakePosition (Transform transform, Vector3 intensity, float duration, bool isLocal, bool useFixedDeltaTime)
            {
                float time = 0.0f;
                Vector3 newRandomPosition = Vector3.zero;
                Vector3 startPosition = isLocal ? transform.localPosition : transform.position;
               
                while (time < duration)
                {
                    time += useFixedDeltaTime ? Time.fixedDeltaTime : Time.deltaTime;

                    newRandomPosition = Random.insideUnitSphere;
                    newRandomPosition.x *= intensity.x;
                    newRandomPosition.y *= intensity.y;
                    newRandomPosition.z *= intensity.z;

                    if (isLocal)
                    {
                        transform.localPosition = startPosition + newRandomPosition;
                    }
                    else
                    {
                        transform.position = startPosition + newRandomPosition;
                    }

                    yield return null;
                }

                if (isLocal)
                {
                    transform.localPosition = startPosition;
                }
                else
                {
                    transform.position = startPosition;
                }
            }

            private static IEnumerator ShakePosition (Transform transform, AXIS axis, float intensity, float duration, bool isLocal, bool useFixedDeltaTime)
            {
                float time = 0.0f;
                Vector3 newRandomPosition = Vector3.zero;
                Vector3 startPosition = isLocal ? transform.localPosition : transform.position;
                
                while (time < duration)
                {
                    time += useFixedDeltaTime ? Time.fixedDeltaTime : Time.deltaTime;

                    newRandomPosition = Random.insideUnitSphere;
                    newRandomPosition *= intensity;

                    switch(axis)
                    {
                        case AXIS.X:
                            newRandomPosition.y = 0.0f;
                            newRandomPosition.z = 0.0f;
                            break;

                        case AXIS.Y:
                            newRandomPosition.x = 0.0f;
                            newRandomPosition.z = 0.0f;
                            break;

                        case AXIS.Z:
                            newRandomPosition.x = 0.0f;
                            newRandomPosition.y = 0.0f;
                            break;

                        case AXIS.X_Y:
                            newRandomPosition.z = 0.0f;
                            break;

                        case AXIS.X_Z:
                            newRandomPosition.y = 0.0f;
                            break;

                        case AXIS.Y_Z:
                            newRandomPosition.x = 0.0f;
                            break;
                    }
                    
                    if (isLocal)
                    {
                        transform.localPosition = startPosition + newRandomPosition;
                    }
                    else
                    {
                        transform.position = startPosition + newRandomPosition;
                    }

                    yield return null;
                }
                
                if (isLocal)
                {
                    transform.localPosition = startPosition;
                }
                else
                {
                    transform.position = startPosition;
                }
            }

            private static IEnumerator ShakePosition(Transform transform, AXIS axis, Vector3 intensity, float duration, bool isLocal, bool useFixedDeltaTime)
            {
                float time = 0.0f;
                Vector3 newRandomPosition = Vector3.zero;
                Vector3 startPosition = isLocal ? transform.localPosition : transform.position;

                while (time < duration)
                {
                    time += useFixedDeltaTime ? Time.fixedDeltaTime : Time.deltaTime;

                    newRandomPosition = Random.insideUnitSphere;
                    
                    switch (axis)
                    {
                        case AXIS.X:
                            newRandomPosition.x *= intensity.x;
                            newRandomPosition.y = 0.0f;
                            newRandomPosition.z = 0.0f;
                            break;

                        case AXIS.Y:
                            newRandomPosition.y *= intensity.y;
                            newRandomPosition.x = 0.0f;
                            newRandomPosition.z = 0.0f;
                            break;

                        case AXIS.Z:
                            newRandomPosition.z *= intensity.z;
                            newRandomPosition.x = 0.0f;
                            newRandomPosition.y = 0.0f;
                            break;

                        case AXIS.X_Y:
                            newRandomPosition.x *= intensity.x;
                            newRandomPosition.y *= intensity.y;
                            newRandomPosition.z = 0.0f;
                            break;

                        case AXIS.X_Z:
                            newRandomPosition.x *= intensity.x;
                            newRandomPosition.z *= intensity.z;
                            newRandomPosition.y = 0.0f;
                            break;

                        case AXIS.Y_Z:
                            newRandomPosition.y *= intensity.y;
                            newRandomPosition.z *= intensity.z;
                            newRandomPosition.x = 0.0f;
                            break;
                    }

                    if (isLocal)
                    {
                        transform.localPosition = startPosition + newRandomPosition;
                    }
                    else
                    {
                        transform.position = startPosition + newRandomPosition;
                    }

                    yield return null;
                }

                if (isLocal)
                {
                    transform.localPosition = startPosition;
                }
                else
                {
                    transform.position = startPosition;
                }
            }

            #endregion

            #endregion

            #region Rotation

            #region WORLD

            public static IEnumerator ShakeRotation(this Transform transform, float intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakeRotation(transform, intensity, duration, false, useFixedDeltaTime);
            }

            public static IEnumerator ShakeRotation(this Transform transform, Vector3 intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakeRotation(transform, intensity, duration, false, useFixedDeltaTime);
            }

            public static IEnumerator ShakeRotationX(this Transform transform, float intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakeRotation(transform, AXIS.X, intensity, duration, false, useFixedDeltaTime);
            }

            public static IEnumerator ShakeRotationY(this Transform transform, float intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakeRotation(transform, AXIS.Y, intensity, duration, false, useFixedDeltaTime);
            }

            public static IEnumerator ShakeRotationZ(this Transform transform, float intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakeRotation(transform, AXIS.Z, intensity, duration, false, useFixedDeltaTime);
            }

            public static IEnumerator ShakeRotationXY(this Transform transform, float intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakeRotation(transform, AXIS.X_Y, intensity, duration, false, useFixedDeltaTime);
            }

            public static IEnumerator ShakeRotationXY(this Transform transform, Vector3 intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakeRotation(transform, AXIS.X_Y, intensity, duration, false, useFixedDeltaTime);
            }

            public static IEnumerator ShakeRotationXZ(this Transform transform, float intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakeRotation(transform, AXIS.X_Z, intensity, duration, false, useFixedDeltaTime);
            }

            public static IEnumerator ShakeRotationXZ(this Transform transform, Vector3 intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakeRotation(transform, AXIS.X_Z, intensity, duration, false, useFixedDeltaTime);
            }

            public static IEnumerator ShakeRotationYZ(this Transform transform, float intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakeRotation(transform, AXIS.Y_Z, intensity, duration, false, useFixedDeltaTime);
            }

            public static IEnumerator ShakeRotationYZ(this Transform transform, Vector3 intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakeRotation(transform, AXIS.Y_Z, intensity, duration, false, useFixedDeltaTime);
            }

            #endregion

            #region LOCAL

            public static IEnumerator ShakeLocalRotation(this Transform transform, float intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakeRotation(transform, intensity, duration, true, useFixedDeltaTime);
            }

            public static IEnumerator ShakeLocalRotation(this Transform transform, Vector3 intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakeRotation(transform, intensity, duration, true, useFixedDeltaTime);
            }

            public static IEnumerator ShakeLocalRotationX(this Transform transform, float intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakeRotation(transform, AXIS.X, intensity, duration, true, useFixedDeltaTime);
            }

            public static IEnumerator ShakeLocalRotationY(this Transform transform, float intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakeRotation(transform, AXIS.Y, intensity, duration, true, useFixedDeltaTime);
            }

            public static IEnumerator ShakeLocalRotationZ(this Transform transform, float intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakeRotation(transform, AXIS.Z, intensity, duration, true, useFixedDeltaTime);
            }

            public static IEnumerator ShakeLocalRotationXY(this Transform transform, float intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakeRotation(transform, AXIS.X_Y, intensity, duration, true, useFixedDeltaTime);
            }

            public static IEnumerator ShakeLocalRotationXY(this Transform transform, Vector3 intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakeRotation(transform, AXIS.X_Y, intensity, duration, true, useFixedDeltaTime);
            }

            public static IEnumerator ShakeLocalRotationXZ(this Transform transform, float intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakeRotation(transform, AXIS.X_Z, intensity, duration, true, useFixedDeltaTime);
            }

            public static IEnumerator ShakeLocalRotationXZ(this Transform transform, Vector3 intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakeRotation(transform, AXIS.X_Z, intensity, duration, true, useFixedDeltaTime);
            }

            public static IEnumerator ShakeLocalRotationYZ(this Transform transform, float intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakeRotation(transform, AXIS.Y_Z, intensity, duration, true, useFixedDeltaTime);
            }

            public static IEnumerator ShakeLocalRotationYZ(this Transform transform, Vector3 intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakeRotation(transform, AXIS.Y_Z, intensity, duration, true, useFixedDeltaTime);
            }

            #endregion

            #region PRIVATES

            private static IEnumerator ShakeRotation(Transform transform, float intensity, float duration, bool isLocal, bool useFixedDeltaTime)
            {
                float time = 0.0f;
                Quaternion startRotation = isLocal ? transform.localRotation : transform.rotation;
                Vector3 startEuLerAngles = isLocal ? transform.localEulerAngles : transform.eulerAngles;

                while (time < duration)
                {
                    time += useFixedDeltaTime ? Time.fixedDeltaTime : Time.deltaTime;
                    
                    if (isLocal)
                    {
                        transform.localRotation = Quaternion.Euler(startEuLerAngles + (Random.insideUnitSphere * intensity));
                    }
                    else
                    {
                        transform.rotation = Quaternion.Euler(startEuLerAngles + (Random.insideUnitSphere * intensity));
                    }

                    yield return null;
                }

                if (isLocal)
                {
                    transform.localRotation = startRotation;
                }
                else
                {
                    transform.rotation = startRotation;
                }
            }

            private static IEnumerator ShakeRotation(Transform transform, Vector3 intensity, float duration, bool isLocal, bool useFixedDeltaTime)
            {
                float time = 0.0f;
                Vector3 newRandomRotation = Vector3.zero;
                Quaternion startRotation = isLocal ? transform.localRotation : transform.rotation;
                Vector3 startEuLerAngles = isLocal ? transform.localEulerAngles : transform.eulerAngles;

                while (time < duration)
                {
                    time += useFixedDeltaTime ? Time.fixedDeltaTime : Time.deltaTime;

                    newRandomRotation = Random.insideUnitSphere;
                    newRandomRotation.x *= intensity.x;
                    newRandomRotation.y *= intensity.y;
                    newRandomRotation.z *= intensity.z;

                    if (isLocal)
                    {
                        transform.localRotation = Quaternion.Euler(startEuLerAngles + newRandomRotation);
                    }
                    else
                    {
                        transform.rotation = Quaternion.Euler(startEuLerAngles + newRandomRotation);
                    }

                    yield return null;
                }

                if (isLocal)
                {
                    transform.localRotation = startRotation;
                }
                else
                {
                    transform.rotation = startRotation;
                }
            }

            private static IEnumerator ShakeRotation(Transform transform, AXIS axis, float intensity, float duration, bool isLocal, bool useFixedDeltaTime)
            {
                float time = 0.0f;
                Vector3 newRandomRotation = Vector3.zero;
                Quaternion startRotation = isLocal ? transform.localRotation : transform.rotation;
                Vector3 startEuLerAngles = isLocal ? transform.localEulerAngles : transform.eulerAngles;

                while (time < duration)
                {
                    time += useFixedDeltaTime ? Time.fixedDeltaTime : Time.deltaTime;

                    newRandomRotation = Random.insideUnitSphere;
                    newRandomRotation *= intensity;
                    
                    switch (axis)
                    {
                        case AXIS.X:
                            newRandomRotation.y = 0.0f;
                            newRandomRotation.z = 0.0f;
                            break;

                        case AXIS.Y:
                            newRandomRotation.x = 0.0f;
                            newRandomRotation.z = 0.0f;
                            break;

                        case AXIS.Z:
                            newRandomRotation.x = 0.0f;
                            newRandomRotation.y = 0.0f;
                            break;

                        case AXIS.X_Y:
                            newRandomRotation.z = 0.0f;
                            break;

                        case AXIS.X_Z:
                            newRandomRotation.y = 0.0f;
                            break;

                        case AXIS.Y_Z:
                            newRandomRotation.x = 0.0f;
                            break;
                    }

                    if (isLocal)
                    {
                        transform.localRotation = Quaternion.Euler(startEuLerAngles + newRandomRotation);
                    }
                    else
                    {
                        transform.rotation = Quaternion.Euler(startEuLerAngles + newRandomRotation);
                    }

                    yield return null;
                }

                if (isLocal)
                {
                    transform.localRotation = startRotation;
                }
                else
                {
                    transform.rotation = startRotation;
                }
            }

            private static IEnumerator ShakeRotation(Transform transform, AXIS axis, Vector3 intensity, float duration, bool isLocal, bool useFixedDeltaTime)
            {
                float time = 0.0f;
                Vector3 newRandomRotation = Vector3.zero;
                Quaternion startRotation = isLocal ? transform.localRotation : transform.rotation;
                Vector3 startEuLerAngles = isLocal ? transform.localEulerAngles : transform.eulerAngles;

                while (time < duration)
                {
                    time += useFixedDeltaTime ? Time.fixedDeltaTime : Time.deltaTime;

                    newRandomRotation = Random.insideUnitSphere;
                    
                    switch (axis)
                    {
                        case AXIS.X:
                            newRandomRotation.x *= intensity.x;
                            newRandomRotation.y = 0.0f;
                            newRandomRotation.z = 0.0f;
                            break;

                        case AXIS.Y:
                            newRandomRotation.y *= intensity.y;
                            newRandomRotation.x = 0.0f;
                            newRandomRotation.z = 0.0f;
                            break;

                        case AXIS.Z:
                            newRandomRotation.z *= intensity.z;
                            newRandomRotation.x = 0.0f;
                            newRandomRotation.y = 0.0f;
                            break;

                        case AXIS.X_Y:
                            newRandomRotation.x *= intensity.x;
                            newRandomRotation.y *= intensity.y;
                            newRandomRotation.z = 0.0f;
                            break;

                        case AXIS.X_Z:
                            newRandomRotation.x *= intensity.x;
                            newRandomRotation.z *= intensity.z;
                            newRandomRotation.y = 0.0f;
                            break;

                        case AXIS.Y_Z:
                            newRandomRotation.y *= intensity.y;
                            newRandomRotation.z *= intensity.z;
                            newRandomRotation.x = 0.0f;
                            break;
                    }

                    if (isLocal)
                    {
                        transform.localRotation = Quaternion.Euler(startEuLerAngles + newRandomRotation);
                    }
                    else
                    {
                        transform.rotation = Quaternion.Euler(startEuLerAngles + newRandomRotation);
                    }

                    yield return null;
                }

                if (isLocal)
                {
                    transform.localRotation = startRotation;
                }
                else
                {
                    transform.rotation = startRotation;
                }
            }

            #endregion

            #endregion

            #region EulerAngles

            #region WORLD

            public static IEnumerator ShakeEuLerAngles(this Transform transform, float intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakeEuLerAngles(transform, intensity, duration, false, useFixedDeltaTime);
            }

            public static IEnumerator ShakeEuLerAngles(this Transform transform, Vector3 intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakeEuLerAngles(transform, intensity, duration, false, useFixedDeltaTime);
            }

            public static IEnumerator ShakeEuLerAnglesX(this Transform transform, float intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakeEuLerAngles(transform, AXIS.X, intensity, duration, false, useFixedDeltaTime);
            }

            public static IEnumerator ShakeEuLerAnglesY(this Transform transform, float intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakeEuLerAngles(transform, AXIS.Y, intensity, duration, false, useFixedDeltaTime);
            }

            public static IEnumerator ShakeEuLerAnglesZ(this Transform transform, float intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakeEuLerAngles(transform, AXIS.Z, intensity, duration, false, useFixedDeltaTime);
            }

            public static IEnumerator ShakeEuLerAnglesXY(this Transform transform, float intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakeEuLerAngles(transform, AXIS.X_Y, intensity, duration, false, useFixedDeltaTime);
            }

            public static IEnumerator ShakeEuLerAnglesXY(this Transform transform, Vector3 intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakeEuLerAngles(transform, AXIS.X_Y, intensity, duration, false, useFixedDeltaTime);
            }

            public static IEnumerator ShakeEuLerAnglesXZ(this Transform transform, float intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakeEuLerAngles(transform, AXIS.X_Z, intensity, duration, false, useFixedDeltaTime);
            }

            public static IEnumerator ShakeEuLerAnglesXZ(this Transform transform, Vector3 intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakeEuLerAngles(transform, AXIS.X_Z, intensity, duration, false, useFixedDeltaTime);
            }

            public static IEnumerator ShakeEuLerAnglesYZ(this Transform transform, float intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakeEuLerAngles(transform, AXIS.Y_Z, intensity, duration, false, useFixedDeltaTime);
            }

            public static IEnumerator ShakeEuLerAnglesYZ(this Transform transform, Vector3 intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakeEuLerAngles(transform, AXIS.Y_Z, intensity, duration, false, useFixedDeltaTime);
            }

            #endregion

            #region LOCAL

            public static IEnumerator ShakeLocalEuLerAngles(this Transform transform, float intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakeEuLerAngles(transform, intensity, duration, true, useFixedDeltaTime);
            }

            public static IEnumerator ShakeLocalEuLerAngles(this Transform transform, Vector3 intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakeEuLerAngles(transform, intensity, duration, true, useFixedDeltaTime);
            }

            public static IEnumerator ShakeLocalEuLerAnglesX(this Transform transform, float intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakeEuLerAngles(transform, AXIS.X, intensity, duration, true, useFixedDeltaTime);
            }

            public static IEnumerator ShakeLocalEuLerAnglesY(this Transform transform, float intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakeEuLerAngles(transform, AXIS.Y, intensity, duration, true, useFixedDeltaTime);
            }

            public static IEnumerator ShakeLocalEuLerAnglesZ(this Transform transform, float intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakeEuLerAngles(transform, AXIS.Z, intensity, duration, true, useFixedDeltaTime);
            }

            public static IEnumerator ShakeLocalEuLerAnglesXY(this Transform transform, float intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakeEuLerAngles(transform, AXIS.X_Y, intensity, duration, true, useFixedDeltaTime);
            }

            public static IEnumerator ShakeLocalEuLerAnglesXY(this Transform transform, Vector3 intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakeEuLerAngles(transform, AXIS.X_Y, intensity, duration, true, useFixedDeltaTime);
            }

            public static IEnumerator ShakeLocalEuLerAnglesXZ(this Transform transform, float intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakeEuLerAngles(transform, AXIS.X_Z, intensity, duration, true, useFixedDeltaTime);
            }

            public static IEnumerator ShakeLocalEuLerAnglesXZ(this Transform transform, Vector3 intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakeEuLerAngles(transform, AXIS.X_Z, intensity, duration, true, useFixedDeltaTime);
            }

            public static IEnumerator ShakeLocalEuLerAnglesYZ(this Transform transform, float intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakeEuLerAngles(transform, AXIS.Y_Z, intensity, duration, true, useFixedDeltaTime);
            }

            public static IEnumerator ShakeLocalEuLerAnglesYZ(this Transform transform, Vector3 intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakeEuLerAngles(transform, AXIS.Y_Z, intensity, duration, true, useFixedDeltaTime);
            }

            #endregion

            #region PRIVATES

            private static IEnumerator ShakeEuLerAngles(Transform transform, float intensity, float duration, bool isLocal, bool useFixedDeltaTime)
            {
                float time = 0.0f;
                Vector3 startEuLerAngles = isLocal ? transform.localEulerAngles : transform.eulerAngles;

                while (time < duration)
                {
                    time += useFixedDeltaTime ? Time.fixedDeltaTime : Time.deltaTime;

                    if (isLocal)
                    {
                        transform.localEulerAngles = startEuLerAngles + (Random.insideUnitSphere * intensity);
                    }
                    else
                    {
                        transform.eulerAngles = startEuLerAngles + (Random.insideUnitSphere * intensity);
                    }

                    yield return null;
                }

                if (isLocal)
                {
                    transform.localEulerAngles = startEuLerAngles;
                }
                else
                {
                    transform.eulerAngles = startEuLerAngles;
                }
            }

            private static IEnumerator ShakeEuLerAngles(Transform transform, Vector3 intensity, float duration, bool isLocal, bool useFixedDeltaTime)
            {
                float time = 0.0f;
                Vector3 newRandomEuLerAngles = Vector3.zero;
                Vector3 startEuLerAngles = isLocal ? transform.localEulerAngles : transform.eulerAngles;

                while (time < duration)
                {
                    time += useFixedDeltaTime ? Time.fixedDeltaTime : Time.deltaTime;

                    newRandomEuLerAngles = Random.insideUnitSphere;
                    newRandomEuLerAngles.x *= intensity.x;
                    newRandomEuLerAngles.y *= intensity.y;
                    newRandomEuLerAngles.z *= intensity.z;

                    if (isLocal)
                    {
                        transform.localEulerAngles = startEuLerAngles + newRandomEuLerAngles;
                    }
                    else
                    {
                        transform.eulerAngles = startEuLerAngles + newRandomEuLerAngles;
                    }

                    yield return null;
                }

                if (isLocal)
                {
                    transform.localEulerAngles = startEuLerAngles;
                }
                else
                {
                    transform.eulerAngles = startEuLerAngles;
                }
            }

            private static IEnumerator ShakeEuLerAngles(Transform transform, AXIS axis, float intensity, float duration, bool isLocal, bool useFixedDeltaTime)
            {
                float time = 0.0f;
                Vector3 newRandomEuLerAngles = Vector3.zero;
                Vector3 startEuLerAngles = isLocal ? transform.localEulerAngles : transform.eulerAngles;

                while (time < duration)
                {
                    time += useFixedDeltaTime ? Time.fixedDeltaTime : Time.deltaTime;

                    newRandomEuLerAngles = Random.insideUnitSphere;
                    newRandomEuLerAngles *= intensity;
                    
                    switch (axis)
                    {
                        case AXIS.X:
                            newRandomEuLerAngles.y = 0.0f;
                            newRandomEuLerAngles.z = 0.0f;
                            break;

                        case AXIS.Y:
                            newRandomEuLerAngles.x = 0.0f;
                            newRandomEuLerAngles.z = 0.0f;
                            break;

                        case AXIS.Z:
                            newRandomEuLerAngles.x = 0.0f;
                            newRandomEuLerAngles.y = 0.0f;
                            break;

                        case AXIS.X_Y:
                            newRandomEuLerAngles.z = 0.0f;
                            break;

                        case AXIS.X_Z:
                            newRandomEuLerAngles.y = 0.0f;
                            break;

                        case AXIS.Y_Z:
                            newRandomEuLerAngles.x = 0.0f;
                            break;
                    }

                    if (isLocal)
                    {

                        transform.localEulerAngles = startEuLerAngles + newRandomEuLerAngles;
                    }
                    else
                    {
                        transform.eulerAngles = startEuLerAngles + newRandomEuLerAngles;
                    }

                    yield return null;
                }

                if (isLocal)
                {
                    transform.localEulerAngles = startEuLerAngles;
                }
                else
                {
                    transform.eulerAngles = startEuLerAngles;
                }
            }

            private static IEnumerator ShakeEuLerAngles(Transform transform, AXIS axis, Vector3 intensity, float duration, bool isLocal, bool useFixedDeltaTime)
            {
                float time = 0.0f;
                Vector3 newRandomEuLerAngles = Vector3.zero;
                Vector3 startEuLerAngles = isLocal ? transform.localEulerAngles : transform.eulerAngles;

                while (time < duration)
                {
                    time += useFixedDeltaTime ? Time.fixedDeltaTime : Time.deltaTime;

                    newRandomEuLerAngles = Random.insideUnitSphere;
                    
                    switch (axis)
                    {
                        case AXIS.X:
                            newRandomEuLerAngles.x *= intensity.x;
                            newRandomEuLerAngles.y = 0.0f;
                            newRandomEuLerAngles.z = 0.0f;
                            break;

                        case AXIS.Y:
                            newRandomEuLerAngles.y *= intensity.y;
                            newRandomEuLerAngles.x = 0.0f;
                            newRandomEuLerAngles.z = 0.0f;
                            break;

                        case AXIS.Z:
                            newRandomEuLerAngles.z *= intensity.z;
                            newRandomEuLerAngles.x = 0.0f;
                            newRandomEuLerAngles.y = 0.0f;
                            break;

                        case AXIS.X_Y:
                            newRandomEuLerAngles.x *= intensity.x;
                            newRandomEuLerAngles.y *= intensity.y;
                            newRandomEuLerAngles.z = 0.0f;
                            break;

                        case AXIS.X_Z:
                            newRandomEuLerAngles.x *= intensity.x;
                            newRandomEuLerAngles.z *= intensity.z;
                            newRandomEuLerAngles.y = 0.0f;
                            break;

                        case AXIS.Y_Z:
                            newRandomEuLerAngles.y *= intensity.y;
                            newRandomEuLerAngles.z *= intensity.z;
                            newRandomEuLerAngles.x = 0.0f;
                            break;
                    }

                    if (isLocal)
                    {

                        transform.localEulerAngles = startEuLerAngles + newRandomEuLerAngles;
                    }
                    else
                    {
                        transform.eulerAngles = startEuLerAngles + newRandomEuLerAngles;
                    }

                    yield return null;
                }

                if (isLocal)
                {
                    transform.localEulerAngles = startEuLerAngles;
                }
                else
                {
                    transform.eulerAngles = startEuLerAngles;
                }
            }

            #endregion
            
            #endregion

            #region Scale

            #region PUBLIC

            public static IEnumerator ShakeScale(this Transform transform, float intensity, float duration, bool useFixedDeltaTime = false)
            {
                return BaseShakeScale(transform, intensity, duration, useFixedDeltaTime);
            }

            public static IEnumerator ShakeScale(this Transform transform, Vector3 intensity, float duration, bool useFixedDeltaTime = false)
            {
                return BaseShakeScale(transform, intensity, duration, useFixedDeltaTime);
            }

            public static IEnumerator ShakeScaleX(this Transform transform, float intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakeScale(transform, AXIS.X, intensity, duration, useFixedDeltaTime);
            }

            public static IEnumerator ShakeScaleY(this Transform transform, float intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakeScale(transform, AXIS.Y, intensity, duration, useFixedDeltaTime);
            }

            public static IEnumerator ShakeScaleZ(this Transform transform, float intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakeScale(transform, AXIS.Z, intensity, duration, useFixedDeltaTime);
            }

            public static IEnumerator ShakeScaleXY(this Transform transform, float intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakeScale(transform, AXIS.X_Y, intensity, duration, useFixedDeltaTime);
            }

            public static IEnumerator ShakeScaleXY(this Transform transform, Vector3 intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakeScale(transform, AXIS.X_Y, intensity, duration, useFixedDeltaTime);
            }

            public static IEnumerator ShakeScaleXZ(this Transform transform, float intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakeScale(transform, AXIS.X_Z, intensity, duration, useFixedDeltaTime);
            }

            public static IEnumerator ShakeScaleXZ(this Transform transform, Vector3 intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakeScale(transform, AXIS.X_Z, intensity, duration, useFixedDeltaTime);
            }

            public static IEnumerator ShakeScaleYZ(this Transform transform, float intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakeScale(transform, AXIS.Y_Z, intensity, duration, useFixedDeltaTime);
            }

            public static IEnumerator ShakeScaleYZ(this Transform transform, Vector3 intensity, float duration, bool useFixedDeltaTime = false)
            {
                return ShakeScale(transform, AXIS.Y_Z, intensity, duration, useFixedDeltaTime);
            }

            #endregion
            
            #region PRIVATES

            private static IEnumerator BaseShakeScale(Transform transform, float intensity, float duration, bool useFixedDeltaTime)
            {
                float time = 0.0f;
                Vector3 startScale = transform.localScale;

                while (time < duration)
                {
                    time += useFixedDeltaTime ? Time.fixedDeltaTime : Time.deltaTime;

                    transform.localScale = startScale + (Random.insideUnitSphere * intensity);

                    yield return null;
                }

                transform.localScale = startScale;
            }

            private static IEnumerator BaseShakeScale(Transform transform, Vector3 intensity, float duration, bool useFixedDeltaTime)
            {
                float time = 0.0f;
                Vector3 newRandomScale = Vector3.zero;
                Vector3 startScale = transform.localScale;

                while (time < duration)
                {
                    time += useFixedDeltaTime ? Time.fixedDeltaTime : Time.deltaTime;

                    newRandomScale = Random.insideUnitSphere;
                    newRandomScale.x *= intensity.x;
                    newRandomScale.y *= intensity.y;
                    newRandomScale.z *= intensity.z;
                    
                    transform.localScale = startScale + newRandomScale;

                    yield return null;
                }

                transform.localScale = startScale;
            }

            private static IEnumerator ShakeScale(Transform transform, AXIS axis, float intensity, float duration, bool useFixedDeltaTime)
            {
                float time = 0.0f;
                Vector3 newRandomScale = Vector3.zero;
                Vector3 startScale = transform.lossyScale;

                while (time < duration)
                {
                    time += useFixedDeltaTime ? Time.fixedDeltaTime : Time.deltaTime;

                    newRandomScale = Random.insideUnitSphere;
                    newRandomScale *= intensity;

                    switch (axis)
                    {
                        case AXIS.X:
                            newRandomScale.y = 0.0f;
                            newRandomScale.z = 0.0f;
                            break;

                        case AXIS.Y:
                            newRandomScale.x = 0.0f;
                            newRandomScale.z = 0.0f;
                            break;

                        case AXIS.Z:
                            newRandomScale.x = 0.0f;
                            newRandomScale.y = 0.0f;
                            break;

                        case AXIS.X_Y:
                            newRandomScale.z = 0.0f;
                            break;

                        case AXIS.X_Z:
                            newRandomScale.y = 0.0f;
                            break;

                        case AXIS.Y_Z:
                            newRandomScale.x = 0.0f;
                            break;
                    }

                    transform.localScale = startScale + newRandomScale;

                    yield return null;
                }

                transform.localScale = startScale;
            }

            private static IEnumerator ShakeScale(Transform transform, AXIS axis, Vector3 intensity, float duration, bool useFixedDeltaTime)
            {
                float time = 0.0f;
                Vector3 newRandomScale = Vector3.zero;
                Vector3 startScale = transform.lossyScale;

                while (time < duration)
                {
                    time += useFixedDeltaTime ? Time.fixedDeltaTime : Time.deltaTime;

                    newRandomScale = Random.insideUnitSphere;

                    switch (axis)
                    {
                        case AXIS.X:
                            newRandomScale.x *= intensity.x;
                            newRandomScale.y = 0.0f;
                            newRandomScale.z = 0.0f;
                            break;

                        case AXIS.Y:
                            newRandomScale.y *= intensity.y;
                            newRandomScale.x = 0.0f;
                            newRandomScale.z = 0.0f;
                            break;

                        case AXIS.Z:
                            newRandomScale.z *= intensity.z;
                            newRandomScale.x = 0.0f;
                            newRandomScale.y = 0.0f;
                            break;

                        case AXIS.X_Y:
                            newRandomScale.x *= intensity.x;
                            newRandomScale.y *= intensity.y;
                            newRandomScale.z = 0.0f;
                            break;

                        case AXIS.X_Z:
                            newRandomScale.x *= intensity.x;
                            newRandomScale.z *= intensity.z;
                            newRandomScale.y = 0.0f;
                            break;

                        case AXIS.Y_Z:
                            newRandomScale.y *= intensity.y;
                            newRandomScale.z *= intensity.z;
                            newRandomScale.x = 0.0f;
                            break;
                    }

                    transform.localScale = startScale + newRandomScale;

                    yield return null;
                }

                transform.localScale = startScale;
            }

            #endregion

            #endregion
        }
    }
}
