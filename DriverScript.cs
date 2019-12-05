using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriverScript : MonoBehaviour
{

    const int ResX = 1024;

    const int ResY = 768;

    Texture2D screen;

    Vector3[] cube = new Vector3[8];

    //Correct = x,y = false
    Vector2 pointA = new Vector2(1f, 1f);
    //Incorrect = x = true, y = false
    Vector2 pointB = new Vector2(2f, 1f);
    //Incorrect = x = false, y = tue
    Vector2 pointC = new Vector2(1f, -2f);

    Vector2 pointD = new Vector2(-2f, -2f);

    Vector2 point1 = new Vector2(1f, 1f);
    Vector2 point2 = new Vector2(-1f, -1f);

    Matrix4x4 allMatrix;
    Matrix4x4 SingleMatrixOfTransformations;

    Matrix4x4 TranslationMatrix;
    Matrix4x4 rotationMatrix;
    Matrix4x4 projectionMatrix;
    Vector3 startingAxis;

    Vector3 cameraLookAt;
    Vector3 cameraPosition;
    Vector3 cameraUP;

    Vector3 LookRotationDirection;
    Quaternion cameraRotation;

    Matrix4x4 viewMatrix;
    Matrix4x4 scaleMatrix;

    Quaternion rotation;

    Vector3[] finalImage;
    Vector2[] dividedPoints;

    Vector2 start;
    Vector2 finish;

    int rotationAngle = -35;

    int translationX = 2;
    int translationY = -2;
    int translationZ = -2;
    int scaleNumber = 5;

    List<Vector2Int> drawnPixels;





    // Start is called before the first frame update
    void Start()
    {
        cube[0] = new Vector3(1, 1, 1);
        cube[1] = new Vector3(-1, 1, 1);
        cube[2] = new Vector3(-1, -1, 1);
        cube[3] = new Vector3(1, -1, 1);

        cube[4] = new Vector3(1, 1, -1);
        cube[5] = new Vector3(-1, 1, -1);
        cube[6] = new Vector3(-1, -1, -1);
        cube[7] = new Vector3(1, -1, -1);

        Vector2Int p1 = new Vector2Int(2, -2);
        Vector2Int p2 = new Vector2Int(-2, 2);
        Breshanham(p1, p2);

        //acceptance test
        Outcode outcodeA = new Outcode(pointA);
        print("pointA: (1,1)");
        outcodeA.printOutcode();

        Outcode outcodeB = new Outcode(pointB);
        print("pointB: (2,1)");
        outcodeB.printOutcode();

        Outcode outcodeC = new Outcode(pointC);
        print("pointC: (1,-2)");
        outcodeC.printOutcode();

        Outcode outcodeD = new Outcode(pointD);
        print("pointD: (-2,-2)");
        outcodeD.printOutcode();

        Outcode inViewPort = new Outcode();

        if ((outcodeA + outcodeB) == inViewPort)
        {
            print("Trival Accepted");
        }
        else if((outcodeA * outcodeB) == inViewPort)
        {
            print("Trival Rejected");
        }

        if(!lineClip(ref point1, ref point2))
        {
            print(point1.x + " , " + point1.y);
            print(point2.x + " , " + point2.y);
        }



        //     print("[1.] 2 lines: (5,15) and (1,-7))");
        Vector2Int pixelPoint1 = new Vector2Int(5, 15);
        Vector2Int pixelPoint2 = new Vector2Int(1, -7);
        List<Vector2Int> list = Breshanham(pixelPoint1, pixelPoint2);

        //foreach(Vector2Int v in list)
        //{
        //    print(v.x + " , " + v.y);
        //}

        //print("[2.] 2 lines: (5,15) and (1,-7))");
        Vector2Int pixelPoint3 = new Vector2Int(25, -20);
        Vector2Int pixelPoint4 = new Vector2Int(0, 15);
        List<Vector2Int> list2 = Breshanham(pixelPoint3, pixelPoint4);

        //foreach (Vector2Int v in list2)
        //{
        //    print(v.x + " , " + v.y);
        //    if (gameObject.tag == "Cube1")
        //    {
        //       //transform.Rotate(new Vector2(v.x, v.y) * Time.deltaTime);
        //    }

        //}

        //Perspective/Projection Matrix
        projectionMatrix = Matrix4x4.Perspective(45, 1.6f, 1, 800);

        //Scale Matrix
        scaleMatrixCall(scaleNumber);



        //texturing




    }


    private void plottingPoints()
    {
        // 0 -1

        start = dividedPoints[0];
        finish = dividedPoints[1];

        if (lineClip(ref start, ref finish))
            plot(Breshanham(convertToScreenSpace(start), convertToScreenSpace(finish)));


        // 1-2

        start = dividedPoints[1];
        finish = dividedPoints[2];

        if (lineClip(ref start, ref finish))
            plot(Breshanham(convertToScreenSpace(start), convertToScreenSpace(finish)));

        // 2-3

        start = dividedPoints[2];
        finish = dividedPoints[3];

        if (lineClip(ref start, ref finish))
            plot(Breshanham(convertToScreenSpace(start), convertToScreenSpace(finish)));

        // 3-0

        start = dividedPoints[3];
        finish = dividedPoints[0];

        if (lineClip(ref start, ref finish))
            plot(Breshanham(convertToScreenSpace(start), convertToScreenSpace(finish)));

        start = dividedPoints[1];
        finish = dividedPoints[5];

        if (lineClip(ref start, ref finish))
            plot(Breshanham(convertToScreenSpace(start), convertToScreenSpace(finish)));

        start = dividedPoints[0];
        finish = dividedPoints[4];

        if (lineClip(ref start, ref finish))
            plot(Breshanham(convertToScreenSpace(start), convertToScreenSpace(finish)));

        start = dividedPoints[2];
        finish = dividedPoints[6];

        if (lineClip(ref start, ref finish))
            plot(Breshanham(convertToScreenSpace(start), convertToScreenSpace(finish)));

        start = dividedPoints[3];
        finish = dividedPoints[7];

        if (lineClip(ref start, ref finish))
            plot(Breshanham(convertToScreenSpace(start), convertToScreenSpace(finish)));

        start = dividedPoints[5];
        finish = dividedPoints[4];

        if (lineClip(ref start, ref finish))
            plot(Breshanham(convertToScreenSpace(start), convertToScreenSpace(finish)));

        start = dividedPoints[4];
        finish = dividedPoints[7];

        if (lineClip(ref start, ref finish))
            plot(Breshanham(convertToScreenSpace(start), convertToScreenSpace(finish)));

        start = dividedPoints[7];
        finish = dividedPoints[6];

        if (lineClip(ref start, ref finish))
            plot(Breshanham(convertToScreenSpace(start), convertToScreenSpace(finish)));

        start = dividedPoints[6];
        finish = dividedPoints[5];

        if (lineClip(ref start, ref finish))
            plot(Breshanham(convertToScreenSpace(start), convertToScreenSpace(finish)));

        screen.Apply();
        // 1 - 5
    }

    public static Vector2 intercept(Vector2 p1, Vector2 p2, int v2)
    {
        float slope = (p2.y - p1.y) / (p2.x - p1.x);

        if (v2 == 0)
        {
            return new Vector2(p1.x + (1 / slope) * (1 - p1.y), 1);
        }

        if (v2 == 1)
        {
            return new Vector2(p1.x + (1 / slope) * (1 - p1.y), -1);
        }

        if (v2 == 2)
        {
            return new Vector2(-1, p1.y + slope * (-1 - p1.x));
        }

        return new Vector2(1, p1.y + slope * (1 - p1.x));
    }


    public static bool lineClip(ref Vector2 v1, ref Vector2 u2)
    {
        Outcode outcodeV = new Outcode();
        Outcode outcodeU = new Outcode();

        outcodeV = new Outcode(v1);
        outcodeU = new Outcode(u2);
        Outcode inViewPort = new Outcode();

        //Acceptance test
        if ((outcodeV + outcodeU) == inViewPort)
        {
            return true;
        }

        //rejection test
        else if (outcodeV * outcodeU != inViewPort)
        {
            return false;
        }

        //If its not accepted or rejected checks if the point is in the viewPort.
        else if (outcodeV == inViewPort)
        {
            return lineClip(ref u2, ref v1);
        }

        else if (outcodeV.up)
        {
            v1 = intercept(u2, v1, 0);

            return false;
        }

        else if (outcodeV.down)
        {
            v1 = intercept(u2, v1, 1);

            return false;
        }

        else if (outcodeV.left)
        {
            v1 = intercept(u2, v1, 2);

            return false;
        }

        v1 = intercept(u2, v1, 3);
        return false;

    }

    public List<Vector2Int> Breshanham(Vector2Int Start, Vector2Int Finish)
    {
        int dx = (Finish.x - Start.x);
        if (dx < 0)
        {
            return Breshanham(Finish, Start);
        }
        int dy = (Finish.y - Start.y);

        if (dy < 0) // negative slope
        {
            return negateY(Breshanham(negateY(Start), negateY(Finish)));
        }
        if (dy > dx) // slope > 1
        {
            return swapXY(Breshanham(swapXY(Start), swapXY(Finish)));
        }
        int A = 2 * dy;         // Should be positive
        int B = 2 * (dy - dx);  // SHould be negative
        int p = 2 * dy - dx;
        List<Vector2Int> outputList = new List<Vector2Int>();

        int y = (int)Start.y;
        for (int x = (int)Start.x; x <= (int)Finish.x; x++)
        {
            outputList.Add(new Vector2Int(x, y));
            if (p > 0)
            {
                y++;
                p += B;
            }
            else
            {
                p += A;
            }


        }


        return outputList;
    }

    private List<Vector2Int> swapXY(List<Vector2Int> list)
    {
        List<Vector2Int> outputList = new List<Vector2Int>();
        foreach (Vector2Int v2 in list)
            outputList.Add(swapXY(v2));

        return outputList;
    }

    private Vector2Int swapXY(Vector2Int v2)
    {
        return new Vector2Int(v2.y, v2.x);
    }

    private List<Vector2Int> negateY(List<Vector2Int> list)
    {
        List<Vector2Int> outputList = new List<Vector2Int>();
        foreach (Vector2Int v in list)
            outputList.Add(negateY(v));

        return outputList;
    }

    private Vector2Int negateY(Vector2Int v)
    {
        return new Vector2Int(v.x, -v.y);
    }


    private void plot(List<Vector2Int> list)
    {
        foreach (Vector2Int pixel in list)
        {
            screen.SetPixel(pixel.x, pixel.y, Color.black);
            //Color[] colors = new Color[1];
           // colors[0] = Color.red;

            //screen.SetPixels(pixel.x, pixel.y, 5, 10, colors);
        }

             
    }

    private Vector2[] divide_by_z(Vector3[] list_of_vertices)
    {
        List<Vector2> output_list = new List<Vector2>();

        foreach(Vector3 v in list_of_vertices)
            output_list.Add(new Vector2(v.x / v.z, v.y / v.z));

        return output_list.ToArray();
    }

    private Vector2Int convertToScreenSpace(Vector2 v)
    {
        int x = (int)Math.Round(((v.x + 1) / 2) * (Screen.width - 1));

        int y = (int)Math.Round(((1 - v.y) / 2) * (Screen.height - 1));

        return new Vector2Int(x, y);
    }

    private Vector3[] MatrixTransform(Vector3[] meshVertices, Matrix4x4 transformMatrix)
    {
        Vector3[] output = new Vector3[meshVertices.Length];
        for (int i = 0; i < meshVertices.Length; i++)
            output[i] = transformMatrix *
                new Vector4(meshVertices[i].x,
                meshVertices[i].y,
                meshVertices[i].z,
                1);

        return output;
    }

    private void rotationMatrixCall(int rotationAngle)
    {
        //rotation matrix
        rotation = Quaternion.AngleAxis(rotationAngle, startingAxis);
        rotationMatrix =
            Matrix4x4.TRS(new Vector3(0, 0, 0),
                            rotation,
                            Vector3.one);
    }

    private void translationMatrixCall(int translationX, int translationY)
    {
        //Translation matrix
        TranslationMatrix =
        Matrix4x4.TRS(new Vector3(translationX, translationY, translationZ),
            Quaternion.identity,
            Vector3.one);
    }

    private void scaleMatrixCall(int scaleNumber)
    {
       scaleMatrix =
       Matrix4x4.TRS(new Vector3(0, 0, 0),
       Quaternion.identity,
       new Vector3(scaleNumber, scaleNumber, scaleNumber));
    }


    // Update is called once per frame
    void Update()
    {


            rotationAngle += 1;



            Destroy(screen);
            screen = new Texture2D(ResX, ResY);
           GetComponent<Renderer>().material.mainTexture = screen;

        // duplicate the original texture and assign to the material
        //Texture2D texture = Instantiate(screen.material.mainTexture) as Texture2D;
        //screen.material.mainTexture = texture;

        // colors used to tint the first 3 mip levels






    startingAxis = new Vector3(5, -3, -3);
            startingAxis.Normalize();

            cameraLookAt = new Vector3(-3, 14, 3);
            cameraPosition = new Vector3(16, 0, 47);
            cameraUP = new Vector3(-2, -3, 14);

            LookRotationDirection = cameraLookAt - cameraPosition;
            cameraRotation = Quaternion.LookRotation(LookRotationDirection.normalized, cameraUP.normalized);

            //View Matrix
            viewMatrix = Matrix4x4.TRS(-cameraPosition, cameraRotation, Vector3.one);

            //Rotation Matrix
            rotationMatrixCall(rotationAngle);

            //Translation matrix
            translationMatrixCall(translationX, translationY);


        //all Matrix
        //Matrix4x4 allMatrix = projectionMatrix * viewMatrix;

        //Single matrix of transformations
        SingleMatrixOfTransformations = TranslationMatrix * scaleMatrix * rotationMatrix;

        //all Matrix + singlematrixoftransformations
        allMatrix = projectionMatrix * viewMatrix * SingleMatrixOfTransformations;


        //final image
        finalImage = MatrixTransform(cube, allMatrix);
        dividedPoints = divide_by_z(finalImage);

        plottingPoints();





    }
}
