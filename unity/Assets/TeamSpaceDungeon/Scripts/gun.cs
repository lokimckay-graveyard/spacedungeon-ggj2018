namespace VRTK.Examples
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class gun : MonoBehaviour
    {

        public GameObject enemyHit;
        public GameObject bulletHole;
        public Transform muzzle;

        // Use this for initialization
        void Start()
        {

            //GetComponentInParent<VRTK_InteractUse>().UseButtonPressed += DoUseOn;
            //GetComponentInParent<VRTK_InteractUse>().UseButtonReleased += DoUseOff;

        }

        // Update is called once per frame
        void Update()
        {

            // if trigger -> shoot gun
            if (Input.GetMouseButtonDown(0))
            {
                shootGun();
            }


        }

        void shootGun()
        {

            RaycastHit hit;

            // Forward vector from Muzzle
            Vector3 fwd = muzzle.TransformDirection(Vector3.forward);

            // Raycast forward from Muzzle, max 25 units
            if (Physics.Raycast(muzzle.transform.position, fwd, out hit, 125))
            {

                // Gunshot sound & animation goes here


                ///////////////////////////////////////


                // If you hit an enemy -> Do something
                if (hit.transform.tag == "enemy")
                {
                    Instantiate(enemyHit, hit.point, Quaternion.identity);
                }

                // For everything else
                else
                {
                    Instantiate(bulletHole, hit.point, Quaternion.identity);
                }


            }
        }

        private void DoUseOn(object sender, ControllerInteractionEventArgs e)
        {
            shootGun();
        }

        private void DoUseOff(object sender, ControllerInteractionEventArgs e)
        {

        }
    }
}
