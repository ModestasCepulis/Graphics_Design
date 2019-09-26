using UnityEngine;
using System.Collections;
using System;
using System.IO;
using UnityEditor;

public class Transformations : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        Vector3[] cube = new Vector3[8];
        cube[0] = new Vector3(1, 1, 1);
        cube[1] = new Vector3(-1, 1, 1);
        cube[2] = new Vector3(-1, -1, 1);
        cube[3] = new Vector3(1, -1, 1);
        cube[4] = new Vector3(1, 1, -1);
        cube[5] = new Vector3(-1, 1, -1);
        cube[6] = new Vector3(-1, -1, -1);
        cube[7] = new Vector3(1, -1, -1);

        Vector3 startingAxis = new Vector3(14, -3, -3);
        startingAxis.Normalize();

        //rotation

        Quaternion rotation = Quaternion.AngleAxis(-13, startingAxis);
        Matrix4x4 rotationMatrix =
            Matrix4x4.TRS(new Vector3(0, 0, 0),
                            rotation,
                            Vector3.one);
        //printMatrix(rotationMatrix);

        Vector3[] imageAfterRotation =
            MatrixTransform(cube, rotationMatrix);
        //  print("Cordinates of image after rotatation");

        //  printVerts(imageAfterRotation);

        //Scaling

        Matrix4x4 scaleMatrix =
    Matrix4x4.TRS(new Vector3(0, 0, 0),
                    Quaternion.identity,
                    new Vector3(14, 5, 3));
        // printMatrix(scaleMatrix);

        Vector3[] imageAfterScale =
            MatrixTransform(imageAfterRotation, scaleMatrix);
        // print("Cordinates of image after scaling");

        //printVerts(imageAfterScale);

        //Translation

        Matrix4x4 TranslationMatrix =
        Matrix4x4.TRS(new Vector3(2, -2, -2),
            Quaternion.identity,
            Vector3.one);
        //print("Translation matrix:");
        // printMatrix(TranslationMatrix);

        Vector3[] imageAfterTranslation =
            MatrixTransform(imageAfterScale, TranslationMatrix);
        //print("Cordinates of image after translation");

        //printVerts(imageAfterTranslation);

        //single Matric of TRS

        Matrix4x4 SingleMatrixOfTransformations = TranslationMatrix * scaleMatrix * rotationMatrix;
        print("Single Matrix Of Transformations:");
        printMatrix(SingleMatrixOfTransformations);

        Vector3[] imageAfterSingleMatrix =
            MatrixTransform(cube, SingleMatrixOfTransformations);
        print("Cordinates of image after super matrix");

        printVerts(imageAfterSingleMatrix);

        //viewing matrix
        Vector3 cameraPosition = new Vector3(16, 0, 47);
        Vector3 cameraLookAt = new Vector3(-3, 14, 3);
        Vector3 cameraUP = new Vector3(-2, -3, 14);

        Vector3 LookRotationDirection = cameraLookAt - cameraPosition;
        Quaternion cameraRotation = Quaternion.LookRotation(LookRotationDirection.normalized, cameraUP.normalized);

        Matrix4x4 viewMatrix = Matrix4x4.TRS(-cameraPosition, cameraRotation, Vector3.one);
       // print("View Matrix");
       // printMatrix(viewMatrix);

        Vector3[] ImageAfterViewing = MatrixTransform(imageAfterTranslation, viewMatrix);
        //print("Image after view matrix:");
        //printVerts(ImageAfterViewing);

        //Projection Matrix
        Matrix4x4 projectionMatrix = Matrix4x4.Perspective(45, 1.6f, 1, 800);
        //print("Projectino Matrix");
        //printMatrix(projectionMatrix);

        Vector3[] ImageAfterProjectionMatrix = MatrixTransform(ImageAfterViewing, projectionMatrix);
        //print("Image after projection matrix");
        //printVerts(ImageAfterProjectionMatrix);

        //SuperMatrixForEverything

        Matrix4x4 SuperMatrix = projectionMatrix * viewMatrix * SingleMatrixOfTransformations;
       // print("Super Matrix");
       // printMatrix(SuperMatrix);

        Vector3[] ImageAfterSuperMatrix =
            MatrixTransform(cube, SuperMatrix);
         //print("Cordinates of image after super matrix");

       // printVerts(ImageAfterSuperMatrix);




    }

    private void printVerts(Vector3[] newImage)
    {
        for (int i = 0; i < newImage.Length; i++)
            print(newImage[i].x + " , " +
                newImage[i].y + " , " +
                newImage[i].z);

    }

    private Vector3[] MatrixTransform(
        Vector3[] meshVertices, 
        Matrix4x4 transformMatrix)
    {
        Vector3[] output = new Vector3[meshVertices.Length];
        for (int i = 0; i < meshVertices.Length; i++)
            output[i] = transformMatrix * 
                new Vector4( 
                meshVertices[i].x,
                meshVertices[i].y,
                meshVertices[i].z,
                    1);

        return output;
    }

    private void printMatrix(Matrix4x4 matrix)
    {
        for (int i = 0; i < 4; i++)
            print(matrix.GetRow(i).ToString());
    }



    // Update is called once per frame
    void Update () {


    }

}
