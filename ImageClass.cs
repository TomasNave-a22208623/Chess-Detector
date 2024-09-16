using System;
using System.Collections.Generic;
using System.Text;
using Emgu.CV.Structure;
using Emgu.CV;
using ZedGraph;
using System.Linq;
using System.Security.Cryptography;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using Emgu.CV.CvEnum;
using System.IO;

namespace CG_OpenCV
{
    class ImageClass
    {

        //-------------------------------------(Negative)--------------------------------------------------------------
        
        // OBG -> Finalizado

        public static void Negative(Image<Bgr, byte> img)
        {

            int x, y;

            Bgr aux;
            for (y = 0; y < img.Height; y++)
            {
                for (x = 0; x < img.Width; x++)
                {
                    // acesso directo : mais lento 
                    aux = img[y, x];
                    img[y, x] = new Bgr(255 - aux.Blue, 255 - aux.Green, 255 - aux.Red);
                }
            }
        }

        //-------------------------------------(ConvertToGray)---------------------------------------------------------

        // Facultativo -> Finalizado

        public static void ConvertToGray(Image<Bgr, byte> img)
        {
            unsafe
            {
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte blue, green, red;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y;

                int step = m.widthStep;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            blue = (dataPtr + x * nChan + y * step)[0];
                            red = (dataPtr + x * nChan + y * step)[2];
                            green = (dataPtr + x * nChan + y * step)[1];

                            (dataPtr + x * nChan + y * step)[0] = (byte)(255 - blue);
                            (dataPtr + x * nChan + y * step)[1] = (byte)(255 - green); ;
                            (dataPtr + x * nChan + y * step)[2] = (byte)(255 - red); ;

                        }

                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        dataPtr += padding;
                    }
                }
            }
        }



        //------------------------------------------(Red Channel)------------------------------------------------------

        // OBG -> Finalizado

        public static void RedChannel(Image<Bgr, byte> img)
        {
            unsafe
            {


                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte blue, green, red;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y;

                int step = m.widthStep;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {


                            (dataPtr + x * nChan + y * step)[0] = (dataPtr + x * nChan + y * step)[2];
                            (dataPtr + x * nChan + y * step)[1] = (dataPtr + x * nChan + y * step)[2];

                        }

                    }
                }
            }
        }

        //------------------------------------------(GreenChannel)------------------------------------------------------

        // OBG -> Finalizado
        public static void GreenChannel(Image<Bgr, byte> img)
        {
            unsafe
            {


                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte blue, green, red;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y;

                int step = m.widthStep;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {


                            (dataPtr + x * nChan + y * step)[0] = (dataPtr + x * nChan + y * step)[1];
                            (dataPtr + x * nChan + y * step)[2] = (dataPtr + x * nChan + y * step)[1];

                        }

                    }
                }
            }
        }

        //------------------------------------------(BlueChannel)------------------------------------------------------

        // OBG -> Finalizado

        public static void BlueChannel(Image<Bgr, byte> img)
        {
            unsafe
            {

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte blue, green, red;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y;

                int step = m.widthStep;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {


                            (dataPtr + x * nChan + y * step)[1] = (dataPtr + x * nChan + y * step)[0];
                            (dataPtr + x * nChan + y * step)[2] = (dataPtr + x * nChan + y * step)[0];

                        }

                    }
                }
            }
        }

        //------------------------------------------(BrightContrast)------------------------------------------------------

        // OBG -> Finalizado

        public static void BrightContrast(Image<Bgr, byte> img, int bright, double contrast)
        {
            unsafe
            {
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                int blue, green, red;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y;

                int step = m.widthStep;

                if (nChan == 3)
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            blue = (int)Math.Round((dataPtr + nChan * x + step * y)[0] * contrast + bright);


                            green = (int)Math.Round((dataPtr + nChan * x + step * y)[1] * contrast + bright);
                            red = (int)Math.Round((dataPtr + nChan * x + step * y)[2] * contrast + bright);

                            if (blue >= 255)
                            {
                                blue = 255;
                            }
                            if (green >= 255)
                            {
                                green = 255;
                            }

                            if (red >= 255)
                            {
                                red = 255;
                            }
                            if (blue < 0)
                            {
                                blue = 0;
                            }
                            if (green < 0)
                            {
                                green = 0;
                            }
                            if (red < 0)
                            {
                                red = 0;
                            }

                            (dataPtr + x * nChan + y * step)[0] = (byte)blue;
                            (dataPtr + x * nChan + y * step)[1] = (byte)green;
                            (dataPtr + x * nChan + y * step)[2] = (byte)red;

                        }


                    }
                }
            }

        }

        //------------------------------------------(Translation)------------------------------------------------------

        // OBG -> Finalizado
        public static void Translation(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, int dx, int dy)
        {
            unsafe
            {


                MIplImage m = img.MIplImage;
                MIplImage mUndo = imgCopy.MIplImage;

                byte* dataPtrimgCopy = (byte*)mUndo.imageData.ToPointer();
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image


                byte blue, green, red;

                int width = img.Width;
                int height = img.Height;
                int nChan = mUndo.nChannels; // number of channels - 3
                int widthstep = mUndo.widthStep;
                int padding = mUndo.widthStep - mUndo.nChannels * mUndo.width; // alinhament bytes (padding)
                int x_o, y_o;




                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {


                        x_o = x - dx;
                        y_o = y - dy;


                        if (x_o < width && x_o >= 0 && y_o < height && y_o >= 0)
                        {
                            blue = (dataPtrimgCopy + y_o * widthstep + x_o * nChan)[0];
                            green = (dataPtrimgCopy + y_o * widthstep + x_o * nChan)[1];
                            red = (dataPtrimgCopy + y_o * widthstep + x_o * nChan)[2];
                        }
                        else
                        {
                            blue = 0;
                            green = 0;
                            red = 0;
                        }

                        (dataPtr + y * widthstep + x * nChan)[0] = blue;
                        (dataPtr + y * widthstep + x * nChan)[1] = green;
                        (dataPtr + y * widthstep + x * nChan)[2] = red;

                    }

                }

            }
        }

        //------------------------------------------(Rotation)------------------------------------------------------

        // OBG -> Finalizado   Core diff : 0,004 
        public static void Rotation(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, float angle)
        {
            unsafe
            {


                MIplImage m = img.MIplImage;
                MIplImage mUndo = imgCopy.MIplImage;

                byte* dataPtrimgCopy = (byte*)mUndo.imageData.ToPointer();
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image


                byte blue, green, red;

                int width = img.Width;
                int height = img.Height;
                int nChan = mUndo.nChannels; // number of channels - 3
                int widthstep = mUndo.widthStep;
                int padding = mUndo.widthStep - mUndo.nChannels * mUndo.width; // alinhament bytes (padding)
                int x_o, y_o;
                double cos = Math.Cos(angle);
                double sen = Math.Sin(angle);




                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {


                        x_o = (int)Math.Round((x - width / 2.0) * cos - (height / 2.0 - y) * sen + width / 2.0);
                        y_o = (int)Math.Round(height / 2.0 - (x - width / 2.0) * sen - (height / 2.0 - y) * cos);


                        byte* dataPtrimgCopy_aux = (byte*)(dataPtrimgCopy + y_o * widthstep + x_o * nChan);

                        if (x_o < width && x_o >= 0 && y_o < height && y_o >= 0)
                        {
                            blue = dataPtrimgCopy_aux[0];
                            green = dataPtrimgCopy_aux[1];
                            red = dataPtrimgCopy_aux[2];
                        }
                        else
                        {
                            blue = 0;
                            green = 0;
                            red = 0;


                            dataPtr[0] = blue;
                            dataPtr[1] = green;
                            dataPtr[2] = red;
                        }

                        (dataPtr + y * widthstep + x * nChan)[0] = blue;
                        (dataPtr + y * widthstep + x * nChan)[1] = green;
                        (dataPtr + y * widthstep + x * nChan)[2] = red;

                    }

                }

            }
        }

        //------------------------------------------(Scale)------------------------------------------------------

        // OBG -> Finalizado

        public static void Scale(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, float scaleFactor)
        {
            unsafe
            {


                MIplImage m = img.MIplImage;
                MIplImage mUndo = imgCopy.MIplImage;

                byte* dataPtrimgCopy = (byte*)mUndo.imageData.ToPointer();
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image


                byte blue, green, red;

                int width = img.Width;
                int height = img.Height;
                int nChan = mUndo.nChannels; // number of channels - 3
                int widthstep = mUndo.widthStep;
                int padding = mUndo.widthStep - mUndo.nChannels * mUndo.width; // alinhament bytes (padding)
                int x_o, y_o;





                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {

                        if (scaleFactor > 1)
                        {
                            x_o = (int)Math.Round(x * scaleFactor);
                            y_o = (int)Math.Round(y * scaleFactor);
                        }
                        else
                        {

                            x_o = (int)Math.Round(x / scaleFactor);
                            y_o = (int)Math.Round(y / scaleFactor);

                        }


                        if (x_o < width && x_o >= 0 && y_o < height && y_o >= 0)
                        {
                            blue = (dataPtrimgCopy + y_o * widthstep + x_o * nChan)[0];
                            green = (dataPtrimgCopy + y_o * widthstep + x_o * nChan)[1];
                            red = (dataPtrimgCopy + y_o * widthstep + x_o * nChan)[2];
                        }
                        else
                        {
                            blue = 0;
                            green = 0;
                            red = 0;



                        }

                        (dataPtr + y * widthstep + x * nChan)[0] = blue;
                        (dataPtr + y * widthstep + x * nChan)[1] = green;
                        (dataPtr + y * widthstep + x * nChan)[2] = red;

                    }

                }

            }
        }

        //------------------------------------------(Scale_point_xy)------------------------------------------------------

        // OBG -> Finalizado

        public static void Scale_point_xy(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, float scaleFactor, int centerX, int centerY)
        {
            unsafe
            {


                MIplImage m = img.MIplImage;
                MIplImage mUndo = imgCopy.MIplImage;

                byte* dataPtrimgCopy = (byte*)mUndo.imageData.ToPointer();
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image


                byte blue, green, red;

                int width = img.Width;
                int height = img.Height;
                int nChan = mUndo.nChannels; // number of channels - 3
                int widthstep = mUndo.widthStep;
                int padding = mUndo.widthStep - mUndo.nChannels * mUndo.width; // alinhament bytes (padding)
                int x_o, y_o;

                int w_half = width / 2;
                int h_half = height / 2;



                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {

                        x_o = (int)Math.Round((x - w_half) / scaleFactor + centerX);

                        y_o = (int)Math.Round((y - h_half) / scaleFactor + centerY);



                        if (x_o < width && x_o >= 0 && y_o < height && y_o >= 0)
                        {
                            blue = (dataPtrimgCopy + y_o * widthstep + x_o * nChan)[0];
                            green = (dataPtrimgCopy + y_o * widthstep + x_o * nChan)[1];
                            red = (dataPtrimgCopy + y_o * widthstep + x_o * nChan)[2];
                        }
                        else

                            blue = red = green = 0;

                        (dataPtr + y * widthstep + x * nChan)[0] = blue;
                        (dataPtr + y * widthstep + x * nChan)[1] = green;
                        (dataPtr + y * widthstep + x * nChan)[2] = red;

                    }

                }

            }
        }


        //---------------------------------------------(Mean)------------------------------------------------------

        // OBG -> Finalizado

        public static void Mean(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy)
        {
            unsafe
            {
                MIplImage m = img.MIplImage; // imagem destino
                MIplImage mUndo = imgCopy.MIplImage; // imagem original

                byte* dataPtrimgCopy = (byte*)mUndo.imageData.ToPointer(); // Pointer to the original image 
                byte* dataPtrImg = (byte*)m.imageData.ToPointer(); // Pointer to the destany image
                int width = imgCopy.Width;
                int height = imgCopy.Height;
                int nChan = mUndo.nChannels;
                int widthstep = mUndo.widthStep;
                int padding = mUndo.widthStep - mUndo.nChannels * mUndo.width; //alinhamento

                int last_h = height - 1;
                int last_w = width - 1;

                // canto superior esquerdo
                dataPtrImg[1] = (byte)Math.Round((4 * dataPtrimgCopy[1] + 2 * (dataPtrimgCopy + nChan)[1] + 2 * (dataPtrimgCopy + m.widthStep)[1] + (dataPtrimgCopy + nChan + m.widthStep)[1]) / 9.0);
                dataPtrImg[2] = (byte)Math.Round((4 * dataPtrimgCopy[2] + 2 * (dataPtrimgCopy + nChan)[2] + 2 * (dataPtrimgCopy + m.widthStep)[2] + (dataPtrimgCopy + nChan + m.widthStep)[2]) / 9.0);
                dataPtrImg[0] = (byte)Math.Round((4 * dataPtrimgCopy[0] + 2 * (dataPtrimgCopy + nChan)[0] + 2 * (dataPtrimgCopy + m.widthStep)[0] + (dataPtrimgCopy + nChan + m.widthStep)[0]) / 9.0);

                dataPtrImg += nChan;
                dataPtrimgCopy += nChan;

                // linha de cima
                for (int x = 1; x < last_w; x++)
                {
                    dataPtrImg[0] = (byte)Math.Round((2 * (dataPtrimgCopy - nChan)[0] + 2 * dataPtrimgCopy[0] + 2 * (dataPtrimgCopy + nChan)[0] + (dataPtrimgCopy - nChan + m.widthStep)[0]
                        + (dataPtrimgCopy + m.widthStep)[0] + (dataPtrimgCopy + nChan + m.widthStep)[0]) / 9.0);
                    dataPtrImg[1] = (byte)Math.Round((2 * (dataPtrimgCopy - nChan)[1] + 2 * dataPtrimgCopy[1] + 2 * (dataPtrimgCopy + nChan)[1] + (dataPtrimgCopy - nChan + m.widthStep)[1]
                        + (dataPtrimgCopy + m.widthStep)[1] + (dataPtrimgCopy + nChan + m.widthStep)[1]) / 9.0);
                    dataPtrImg[2] = (byte)Math.Round((2 * (dataPtrimgCopy - nChan)[2] + 2 * dataPtrimgCopy[2] + 2 * (dataPtrimgCopy + nChan)[2] + (dataPtrimgCopy - nChan + m.widthStep)[2]
                        + (dataPtrimgCopy + m.widthStep)[2] + (dataPtrimgCopy + nChan + m.widthStep)[2]) / 9.0);

                    // advance pointer to next pixel
                    dataPtrImg += nChan;
                    dataPtrimgCopy += nChan;
                }

                // canto superior direito
                dataPtrImg[1] = (byte)Math.Round((4 * dataPtrimgCopy[1] + 2 * (dataPtrimgCopy - nChan)[1] + 2 * (dataPtrimgCopy + m.widthStep)[1] + (dataPtrimgCopy - nChan + m.widthStep)[1]) / 9.0);
                dataPtrImg[2] = (byte)Math.Round((4 * dataPtrimgCopy[2] + 2 * (dataPtrimgCopy - nChan)[2] + 2 * (dataPtrimgCopy + m.widthStep)[2] + (dataPtrimgCopy - nChan + m.widthStep)[2]) / 9.0);
                dataPtrImg[0] = (byte)Math.Round((4 * dataPtrimgCopy[0] + 2 * (dataPtrimgCopy - nChan)[0] + 2 * (dataPtrimgCopy + m.widthStep)[0] + (dataPtrimgCopy - nChan + m.widthStep)[0]) / 9.0);

                dataPtrImg += nChan;
                dataPtrimgCopy += nChan;
                dataPtrImg += padding;
                dataPtrimgCopy += padding;

                //### margem esquerda + tratamento do interior da imagem + margem direita
                //Percorre a imagem
                for (int y = 1; y < last_h; y++)
                {
                    //pixel esquerda da linha
                    dataPtrImg[0] = (byte)Math.Round((2 * (dataPtrimgCopy - m.widthStep)[0] + (dataPtrimgCopy + nChan - m.widthStep)[0] + 2 * (dataPtrimgCopy)[0] + (dataPtrimgCopy + nChan)[0]
                        + 2 * (dataPtrimgCopy + m.widthStep)[0] + (dataPtrimgCopy + nChan + m.widthStep)[0]) / 9.0);
                    dataPtrImg[1] = (byte)Math.Round((2 * (dataPtrimgCopy - m.widthStep)[1] + (dataPtrimgCopy + nChan - m.widthStep)[1] + 2 * (dataPtrimgCopy)[1] + (dataPtrimgCopy + nChan)[1]
                        + 2 * (dataPtrimgCopy + m.widthStep)[1] + (dataPtrimgCopy + nChan + m.widthStep)[1]) / 9.0);
                    dataPtrImg[2] = (byte)Math.Round((2 * (dataPtrimgCopy - m.widthStep)[2] + (dataPtrimgCopy + nChan - m.widthStep)[2] + 2 * (dataPtrimgCopy)[2] + (dataPtrimgCopy + nChan)[2]
                        + 2 * (dataPtrimgCopy + m.widthStep)[2] + (dataPtrimgCopy + nChan + m.widthStep)[2]) / 9.0);

                    dataPtrImg += nChan;
                    dataPtrimgCopy += nChan;

                    for (int x = 1; x < last_w; x++)
                    {
                        dataPtrImg[0] = (byte)Math.Round(((dataPtrimgCopy - nChan - m.widthStep)[0] + (dataPtrimgCopy - m.widthStep)[0] + (dataPtrimgCopy + nChan - m.widthStep)[0]
                            + (dataPtrimgCopy - nChan)[0] + dataPtrimgCopy[0] + (dataPtrimgCopy + nChan)[0] + (dataPtrimgCopy - nChan + m.widthStep)[0]
                            + (dataPtrimgCopy + m.widthStep)[0] + (dataPtrimgCopy + nChan + m.widthStep)[0]) / 9.0);
                        dataPtrImg[1] = (byte)Math.Round(((dataPtrimgCopy - nChan - m.widthStep)[1] + (dataPtrimgCopy - m.widthStep)[1] + (dataPtrimgCopy + nChan - m.widthStep)[1]
                            + (dataPtrimgCopy - nChan)[1] + dataPtrimgCopy[1] + (dataPtrimgCopy + nChan)[1] + (dataPtrimgCopy - nChan + m.widthStep)[1]
                            + (dataPtrimgCopy + m.widthStep)[1] + (dataPtrimgCopy + nChan + m.widthStep)[1]) / 9.0);
                        dataPtrImg[2] = (byte)Math.Round(((dataPtrimgCopy - nChan - m.widthStep)[2] + (dataPtrimgCopy - m.widthStep)[2] + (dataPtrimgCopy + nChan - m.widthStep)[2]
                            + (dataPtrimgCopy - nChan)[2] + dataPtrimgCopy[2] + (dataPtrimgCopy + nChan)[2] + (dataPtrimgCopy - nChan + m.widthStep)[2]
                            + (dataPtrimgCopy + m.widthStep)[2] + (dataPtrimgCopy + nChan + m.widthStep)[2]) / 9.0);

                        // advance the pointer to the next pixel
                        dataPtrImg += nChan;
                        dataPtrimgCopy += nChan;
                    }

                    //pixel direito de linha
                    dataPtrImg[0] = (byte)Math.Round(((dataPtrimgCopy - nChan - m.widthStep)[0] + 2 * (dataPtrimgCopy - m.widthStep)[0] + 2 * (dataPtrimgCopy)[0]
                           + (dataPtrimgCopy - nChan)[0] + (dataPtrimgCopy - nChan + m.widthStep)[0] + 2 * (dataPtrimgCopy + m.widthStep)[0]) / 9.0);
                    dataPtrImg[1] = (byte)Math.Round(((dataPtrimgCopy - nChan - m.widthStep)[1] + 2 * (dataPtrimgCopy - m.widthStep)[1] + 2 * (dataPtrimgCopy)[1]
                           + (dataPtrimgCopy - nChan)[1] + (dataPtrimgCopy - nChan + m.widthStep)[1] + 2 * (dataPtrimgCopy + m.widthStep)[1]) / 9.0);
                    dataPtrImg[2] = (byte)Math.Round(((dataPtrimgCopy - nChan - m.widthStep)[2] + 2 * (dataPtrimgCopy - m.widthStep)[2] + 2 * (dataPtrimgCopy)[2]
                           + (dataPtrimgCopy - nChan)[2] + (dataPtrimgCopy - nChan + m.widthStep)[2] + 2 * (dataPtrimgCopy + m.widthStep)[2]) / 9.0);

                    //at the end of the line advance the pointer by the aligment bytes padding
                    dataPtrImg += nChan;
                    dataPtrimgCopy += nChan;
                    dataPtrImg += padding;
                    dataPtrimgCopy += padding;
                }

                // canto inferior esquerdo
                dataPtrImg[1] = (byte)Math.Round((4 * dataPtrimgCopy[1] + 2 * (dataPtrimgCopy + nChan)[1] + 2 * (dataPtrimgCopy - m.widthStep)[1] + (dataPtrimgCopy + nChan - m.widthStep)[1]) / 9.0);
                dataPtrImg[2] = (byte)Math.Round((4 * dataPtrimgCopy[2] + 2 * (dataPtrimgCopy + nChan)[2] + 2 * (dataPtrimgCopy - m.widthStep)[2] + (dataPtrimgCopy + nChan - m.widthStep)[2]) / 9.0);
                dataPtrImg[0] = (byte)Math.Round((4 * dataPtrimgCopy[0] + 2 * (dataPtrimgCopy + nChan)[0] + 2 * (dataPtrimgCopy - m.widthStep)[0] + (dataPtrimgCopy + nChan - m.widthStep)[0]) / 9.0);

                // advance th epointer to next pixel
                dataPtrImg += nChan;
                dataPtrimgCopy += nChan;

                // linha de baixo
                for (int x = 1; x < last_w; x++)
                {
                    dataPtrImg[0] = (byte)Math.Round((2 * (dataPtrimgCopy - nChan)[0]
                        + 2 * dataPtrimgCopy[0] + 2 * (dataPtrimgCopy + nChan)[0] + (dataPtrimgCopy - nChan - m.widthStep)[0] + (dataPtrimgCopy - m.widthStep)[0]
                        + (dataPtrimgCopy + nChan - m.widthStep)[0]) / 9.0);
                    dataPtrImg[1] = (byte)Math.Round((2 * (dataPtrimgCopy - nChan)[1]
                        + 2 * dataPtrimgCopy[1] + 2 * (dataPtrimgCopy + nChan)[1] + (dataPtrimgCopy - nChan - m.widthStep)[1] + (dataPtrimgCopy - m.widthStep)[1]
                        + (dataPtrimgCopy + nChan - m.widthStep)[1]) / 9.0);
                    dataPtrImg[2] = (byte)Math.Round((2 * (dataPtrimgCopy - nChan)[2]
                        + 2 * dataPtrimgCopy[2] + 2 * (dataPtrimgCopy + nChan)[2] + (dataPtrimgCopy - nChan - m.widthStep)[2] + (dataPtrimgCopy - m.widthStep)[2]
                        + (dataPtrimgCopy + nChan - m.widthStep)[2]) / 9.0);

                    // advance th pointer to next pixel
                    dataPtrImg += nChan;
                    dataPtrimgCopy += nChan;
                }

                //canto inferior direito
                dataPtrImg[0] = (byte)Math.Round((4 * dataPtrimgCopy[0] + 2 * (dataPtrimgCopy - nChan)[0] + 2 * (dataPtrimgCopy - m.widthStep)[0] + (dataPtrimgCopy - nChan - m.widthStep)[0]) / 9.0);
                dataPtrImg[1] = (byte)Math.Round((4 * dataPtrimgCopy[1] + 2 * (dataPtrimgCopy - nChan)[1] + 2 * (dataPtrimgCopy - m.widthStep)[1] + (dataPtrimgCopy - nChan - m.widthStep)[1]) / 9.0);
                dataPtrImg[2] = (byte)Math.Round((4 * dataPtrimgCopy[2] + 2 * (dataPtrimgCopy - nChan)[2] + 2 * (dataPtrimgCopy - m.widthStep)[2] + (dataPtrimgCopy - nChan - m.widthStep)[2]) / 9.0);
            }
        }


        //------------------------------------------(NonUniform)------------------------------------------------------

        // OBG -> BorderDiff(0,099)

        public static void NonUniform(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, float[,] matrix, float matrixWeight)
        {
            unsafe
            {
                MIplImage m = img.MIplImage; // imagem destino
                MIplImage mUndo = imgCopy.MIplImage; // imagem original

                byte* dataPtrimgCopy = (byte*)mUndo.imageData.ToPointer(); // Pointer to the original image 
                byte* dataPtrImg = (byte*)m.imageData.ToPointer(); // Pointer to the destany image
                int width = imgCopy.Width;
                int height = imgCopy.Height;
                int nChan = mUndo.nChannels;
                int widthstep = mUndo.widthStep;
                int padding = mUndo.widthStep - mUndo.nChannels * mUndo.width; //alinhamento
                int xd, yd;

                int last_h = height - 1;
                int last_w = width - 1;

                float blue;
                float green;
                float red;

                if (nChan == 3) // image RGB 
                {
                    // canto superior esquerdo
                    blue = ((matrix[0, 0] * dataPtrimgCopy[0]
                        + matrix[1, 0] * dataPtrimgCopy[0]
                        + matrix[2, 0] * (dataPtrimgCopy + nChan)[0]
                        + matrix[0, 1] * dataPtrimgCopy[0]
                        + matrix[1, 1] * dataPtrimgCopy[0]
                        + matrix[2, 1] * (dataPtrimgCopy + nChan)[0]
                        + matrix[0, 2] * (dataPtrimgCopy + m.widthStep)[0]
                        + matrix[1, 2] * (dataPtrimgCopy + m.widthStep)[0]
                        + matrix[2, 2] * (dataPtrimgCopy + nChan + m.widthStep)[0]) / matrixWeight);
                    green = ((matrix[0, 0] * dataPtrimgCopy[1]
                        + matrix[1, 0] * dataPtrimgCopy[1]
                        + matrix[2, 0] * (dataPtrimgCopy + nChan)[1]
                        + matrix[0, 1] * dataPtrimgCopy[1]
                        + matrix[1, 1] * dataPtrimgCopy[1]
                        + matrix[2, 1] * (dataPtrimgCopy + nChan)[1]
                        + matrix[0, 2] * (dataPtrimgCopy + m.widthStep)[1]
                        + matrix[1, 2] * (dataPtrimgCopy + m.widthStep)[1]
                        + matrix[2, 2] * (dataPtrimgCopy + nChan + m.widthStep)[1]) / matrixWeight);
                    red = ((matrix[0, 0] * dataPtrimgCopy[2]
                        + matrix[1, 0] * dataPtrimgCopy[2]
                        + matrix[2, 0] * (dataPtrimgCopy + nChan)[2]
                        + matrix[0, 1] * dataPtrimgCopy[2]
                        + matrix[1, 1] * dataPtrimgCopy[2]
                        + matrix[2, 1] * (dataPtrimgCopy + nChan)[2]
                        + matrix[0, 2] * (dataPtrimgCopy + m.widthStep)[2]
                        + matrix[1, 2] * (dataPtrimgCopy + m.widthStep)[2]
                        + matrix[2, 2] * (dataPtrimgCopy + nChan + m.widthStep)[2]) / matrixWeight);

                    blue = (int)Math.Round(blue);
                    green = (int)Math.Round(green);
                    red = (int)Math.Round(red);
                    if (blue >= 255)
                        blue = 255;
                    if (green >= 255)
                        green = 255;
                    if (red >= 255)
                        red = 255;
                    if (blue <= 0)
                        blue = 0;
                    if (green <= 0)
                        green = 0;
                    if (red <= 0)
                        red = 0;

                    dataPtrImg[0] = (byte)blue;
                    dataPtrImg[1] = (byte)green;
                    dataPtrImg[2] = (byte)red;

                    dataPtrImg += nChan;
                    dataPtrimgCopy += nChan;

                    // linha de cima
                    for (xd = 1; xd < last_w; xd++)
                    {
                        blue = (((matrix[0, 0] + matrix[1, 0]) * (dataPtrimgCopy - nChan)[0]
                            + (matrix[1, 0] + matrix[1, 1]) * dataPtrimgCopy[0]
                            + (matrix[2, 0] + matrix[2, 1]) * (dataPtrimgCopy + nChan)[0]
                            + matrix[0, 2] * (dataPtrimgCopy - nChan + m.widthStep)[0]
                            + matrix[1, 2] * (dataPtrimgCopy + m.widthStep)[0]
                            + matrix[2, 2] * (dataPtrimgCopy + nChan + m.widthStep)[0]) / matrixWeight);
                        green = (((matrix[0, 0] + matrix[1, 0]) * (dataPtrimgCopy - nChan)[1]
                            + (matrix[1, 0] + matrix[1, 1]) * dataPtrimgCopy[1]
                            + (matrix[2, 0] + matrix[2, 1]) * (dataPtrimgCopy + nChan)[1]
                            + matrix[0, 2] * (dataPtrimgCopy - nChan + m.widthStep)[1]
                            + matrix[1, 2] * (dataPtrimgCopy + m.widthStep)[1]
                            + matrix[2, 2] * (dataPtrimgCopy + nChan + m.widthStep)[1]) / matrixWeight);
                        red = (((matrix[0, 0] + matrix[1, 0]) * (dataPtrimgCopy - nChan)[2]
                            + (matrix[1, 0] + matrix[1, 1]) * dataPtrimgCopy[2]
                            + (matrix[2, 0] + matrix[2, 1]) * (dataPtrimgCopy + nChan)[2]
                            + matrix[0, 2] * (dataPtrimgCopy - nChan + m.widthStep)[2]
                            + matrix[1, 2] * (dataPtrimgCopy + m.widthStep)[2]
                            + matrix[2, 2] * (dataPtrimgCopy + nChan + m.widthStep)[2]) / matrixWeight);

                        blue = (int)Math.Round(blue);
                        green = (int)Math.Round(green);
                        red = (int)Math.Round(red);
                        if (blue >= 255)
                            blue = 255;
                        if (green >= 255)
                            green = 255;
                        if (red >= 255)
                            red = 255;
                        if (blue <= 0)
                            blue = 0;
                        if (green <= 0)
                            green = 0;
                        if (red <= 0)
                            red = 0;

                        dataPtrImg[0] = (byte)blue;
                        dataPtrImg[1] = (byte)green;
                        dataPtrImg[2] = (byte)red;
                        // advance pointer to the next pixel
                        dataPtrImg += nChan;
                        dataPtrimgCopy += nChan;
                    }
                    // canto superior direito
                    blue = ((matrix[0, 0] * (dataPtrimgCopy - nChan)[0]
                        + matrix[1, 0] * dataPtrimgCopy[0]
                        + matrix[2, 0] * dataPtrimgCopy[0]
                        + matrix[0, 1] * (dataPtrimgCopy - nChan)[0]
                        + matrix[1, 1] * dataPtrimgCopy[0]
                        + matrix[2, 1] * dataPtrimgCopy[0]
                        + matrix[0, 2] * (dataPtrimgCopy - nChan + m.widthStep)[0]
                        + matrix[1, 2] * (dataPtrimgCopy + m.widthStep)[0]
                        + matrix[2, 2] * (dataPtrimgCopy + nChan + m.widthStep)[0]) / matrixWeight);
                    green = ((matrix[0, 0] * (dataPtrimgCopy - nChan)[1]
                        + matrix[1, 0] * dataPtrimgCopy[1]
                        + matrix[2, 0] * dataPtrimgCopy[1]
                        + matrix[0, 1] * (dataPtrimgCopy - nChan)[1]
                        + matrix[1, 1] * dataPtrimgCopy[1]
                        + matrix[2, 1] * dataPtrimgCopy[1]
                        + matrix[0, 2] * (dataPtrimgCopy - nChan + m.widthStep)[1]
                        + matrix[1, 2] * (dataPtrimgCopy + m.widthStep)[1]
                        + matrix[2, 2] * (dataPtrimgCopy + nChan + m.widthStep)[1]) / matrixWeight);
                    red = ((matrix[0, 0] * (dataPtrimgCopy - nChan)[2]
                        + matrix[1, 0] * dataPtrimgCopy[2]
                        + matrix[2, 0] * dataPtrimgCopy[2]
                        + matrix[0, 1] * (dataPtrimgCopy - nChan)[2]
                        + matrix[1, 1] * dataPtrimgCopy[2]
                        + matrix[2, 1] * dataPtrimgCopy[2]
                        + matrix[0, 2] * (dataPtrimgCopy - nChan + m.widthStep)[2]
                        + matrix[1, 2] * (dataPtrimgCopy + m.widthStep)[2]
                        + matrix[2, 2] * (dataPtrimgCopy + nChan + m.widthStep)[2]) / matrixWeight);

                    blue = (int)Math.Round(blue);
                    green = (int)Math.Round(green);
                    red = (int)Math.Round(red);
                    if (blue >= 255)
                        blue = 255;
                    if (green >= 255)
                        green = 255;
                    if (red >= 255)
                        red = 255;
                    if (blue <= 0)
                        blue = 0;
                    if (green <= 0)
                        green = 0;
                    if (red <= 0)
                        red = 0;

                    dataPtrImg[0] = (byte)blue;
                    dataPtrImg[1] = (byte)green;
                    dataPtrImg[2] = (byte)red;

                    dataPtrImg = dataPtrImg + nChan + padding;
                    dataPtrimgCopy = dataPtrimgCopy + nChan + padding;

                    for (yd = 1; yd < last_h; yd++)
                    {
                        // pixel esquerdo da linha
                        blue = (int)((matrix[0, 0] * (dataPtrimgCopy - m.widthStep)[0]
                           + matrix[1, 0] * (dataPtrimgCopy - m.widthStep)[0]
                           + matrix[2, 0] * (dataPtrimgCopy + nChan - m.widthStep)[0]
                           + matrix[0, 1] * dataPtrimgCopy[0]
                           + matrix[1, 1] * dataPtrimgCopy[0]
                           + matrix[2, 1] * (dataPtrimgCopy + nChan)[0]
                           + matrix[0, 2] * (dataPtrimgCopy + m.widthStep)[0]
                           + matrix[1, 2] * (dataPtrimgCopy + m.widthStep)[0]
                           + matrix[2, 2] * (dataPtrimgCopy + nChan + m.widthStep)[0]) / matrixWeight);
                        green = (int)((matrix[0, 0] * (dataPtrimgCopy - m.widthStep)[1]
                           + matrix[1, 0] * (dataPtrimgCopy - m.widthStep)[1]
                           + matrix[2, 0] * (dataPtrimgCopy + nChan - m.widthStep)[1]
                           + matrix[0, 1] * dataPtrimgCopy[1]
                           + matrix[1, 1] * dataPtrimgCopy[1]
                           + matrix[2, 1] * (dataPtrimgCopy + nChan)[1]
                           + matrix[0, 2] * (dataPtrimgCopy + m.widthStep)[1]
                           + matrix[1, 2] * (dataPtrimgCopy + m.widthStep)[1]
                           + matrix[2, 2] * (dataPtrimgCopy + nChan + m.widthStep)[1]) / matrixWeight);
                        red = (int)((matrix[0, 0] * (dataPtrimgCopy - m.widthStep)[2]
                           + matrix[1, 0] * (dataPtrimgCopy - m.widthStep)[2]
                           + matrix[2, 0] * (dataPtrimgCopy + nChan - m.widthStep)[2]
                           + matrix[0, 1] * dataPtrimgCopy[2]
                           + matrix[1, 1] * dataPtrimgCopy[2]
                           + matrix[2, 1] * (dataPtrimgCopy + nChan)[2]
                           + matrix[0, 2] * (dataPtrimgCopy + m.widthStep)[2]
                           + matrix[1, 2] * (dataPtrimgCopy + m.widthStep)[2]
                           + matrix[2, 2] * (dataPtrimgCopy + nChan + m.widthStep)[2]) / matrixWeight);

                        blue = (int)Math.Round(blue);
                        green = (int)Math.Round(green);
                        red = (int)Math.Round(red);
                        if (blue >= 255)
                            blue = 255;
                        if (green >= 255)
                            green = 255;
                        if (red >= 255)
                            red = 255;
                        if (blue <= 0)
                            blue = 0;
                        if (green <= 0)
                            green = 0;
                        if (red <= 0)
                            red = 0;

                        dataPtrImg[0] = (byte)blue;
                        dataPtrImg[1] = (byte)green;
                        dataPtrImg[2] = (byte)red;

                        dataPtrImg += nChan;
                        dataPtrimgCopy += nChan;

                        // imagem sem o contorno
                        for (xd = 1; xd < last_w; xd++)
                        {
                            blue = ((matrix[0, 0] * (dataPtrimgCopy - nChan - m.widthStep)[0]
                               + matrix[1, 0] * (dataPtrimgCopy - m.widthStep)[0]
                               + matrix[2, 0] * (dataPtrimgCopy + nChan - m.widthStep)[0]
                               + matrix[0, 1] * (dataPtrimgCopy - nChan)[0]
                               + matrix[1, 1] * dataPtrimgCopy[0]
                               + matrix[2, 1] * (dataPtrimgCopy + nChan)[0]
                               + matrix[0, 2] * (dataPtrimgCopy - nChan + m.widthStep)[0]
                               + matrix[1, 2] * (dataPtrimgCopy + m.widthStep)[0]
                               + matrix[2, 2] * (dataPtrimgCopy + nChan + m.widthStep)[0]) / matrixWeight);
                            green = ((matrix[0, 0] * (dataPtrimgCopy - nChan - m.widthStep)[1]
                               + matrix[1, 0] * (dataPtrimgCopy - m.widthStep)[1]
                               + matrix[2, 0] * (dataPtrimgCopy + nChan - m.widthStep)[1]
                               + matrix[0, 1] * (dataPtrimgCopy - nChan)[1]
                               + matrix[1, 1] * dataPtrimgCopy[1]
                               + matrix[2, 1] * (dataPtrimgCopy + nChan)[1]
                               + matrix[0, 2] * (dataPtrimgCopy - nChan + m.widthStep)[1]
                               + matrix[1, 2] * (dataPtrimgCopy + m.widthStep)[1]
                               + matrix[2, 2] * (dataPtrimgCopy + nChan + m.widthStep)[1]) / matrixWeight);
                            red = ((matrix[0, 0] * (dataPtrimgCopy - nChan - m.widthStep)[2]
                               + matrix[1, 0] * (dataPtrimgCopy - m.widthStep)[2]
                               + matrix[2, 0] * (dataPtrimgCopy + nChan - m.widthStep)[2]
                               + matrix[0, 1] * (dataPtrimgCopy - nChan)[2]
                               + matrix[1, 1] * dataPtrimgCopy[2]
                               + matrix[2, 1] * (dataPtrimgCopy + nChan)[2]
                               + matrix[0, 2] * (dataPtrimgCopy - nChan + m.widthStep)[2]
                               + matrix[1, 2] * (dataPtrimgCopy + m.widthStep)[2]
                               + matrix[2, 2] * (dataPtrimgCopy + nChan + m.widthStep)[2]) / matrixWeight);

                            blue = (int)Math.Round(blue);
                            green = (int)Math.Round(green);
                            red = (int)Math.Round(red);
                            if (blue >= 255)
                                blue = 255;
                            if (green >= 255)
                                green = 255;
                            if (red >= 255)
                                red = 255;
                            if (blue <= 0)
                                blue = 0;
                            if (green <= 0)
                                green = 0;
                            if (red <= 0)
                                red = 0;

                            dataPtrImg[0] = (byte)blue;
                            dataPtrImg[1] = (byte)green;
                            dataPtrImg[2] = (byte)red;

                            // advance the pointer to the next pixel
                            dataPtrImg += nChan;
                            dataPtrimgCopy += nChan;
                        }

                        // pixel direito da linha
                        blue = ((matrix[0, 0] * (dataPtrimgCopy - nChan - m.widthStep)[0]
                               + matrix[1, 0] * (dataPtrimgCopy - m.widthStep)[0]
                               + matrix[2, 0] * (dataPtrimgCopy - m.widthStep)[0]
                               + matrix[0, 1] * (dataPtrimgCopy - nChan)[0]
                               + matrix[1, 1] * dataPtrimgCopy[0]
                               + matrix[2, 1] * dataPtrimgCopy[0]
                               + matrix[0, 2] * (dataPtrimgCopy - nChan + m.widthStep)[0]
                               + matrix[1, 2] * (dataPtrimgCopy + m.widthStep)[0]
                               + matrix[2, 2] * (dataPtrimgCopy + m.widthStep)[0]) / matrixWeight);
                        green = ((matrix[0, 0] * (dataPtrimgCopy - nChan - m.widthStep)[1]
                               + matrix[1, 0] * (dataPtrimgCopy - m.widthStep)[1]
                               + matrix[2, 0] * (dataPtrimgCopy - m.widthStep)[1]
                               + matrix[0, 1] * (dataPtrimgCopy - nChan)[1]
                               + matrix[1, 1] * dataPtrimgCopy[1]
                               + matrix[2, 1] * dataPtrimgCopy[1]
                               + matrix[0, 2] * (dataPtrimgCopy - nChan + m.widthStep)[1]
                               + matrix[1, 2] * (dataPtrimgCopy + m.widthStep)[1]
                               + matrix[2, 2] * (dataPtrimgCopy + m.widthStep)[1]) / matrixWeight);
                        red = ((matrix[0, 0] * (dataPtrimgCopy - nChan - m.widthStep)[2]
                               + matrix[1, 0] * (dataPtrimgCopy - m.widthStep)[2]
                               + matrix[2, 0] * (dataPtrimgCopy - m.widthStep)[2]
                               + matrix[0, 1] * (dataPtrimgCopy - nChan)[2]
                               + matrix[1, 1] * dataPtrimgCopy[2]
                               + matrix[2, 1] * dataPtrimgCopy[2]
                               + matrix[0, 2] * (dataPtrimgCopy - nChan + m.widthStep)[2]
                               + matrix[1, 2] * (dataPtrimgCopy + m.widthStep)[2]
                               + matrix[2, 2] * (dataPtrimgCopy + m.widthStep)[2]) / matrixWeight);

                        blue = (int)Math.Round(blue);
                        green = (int)Math.Round(green);
                        red = (int)Math.Round(red);
                        if (blue >= 255)
                            blue = 255;
                        if (green >= 255)
                            green = 255;
                        if (red >= 255)
                            red = 255;
                        if (blue <= 0)
                            blue = 0;
                        if (green <= 0)
                            green = 0;
                        if (red <= 0)
                            red = 0;

                        dataPtrImg[0] = (byte)blue;
                        dataPtrImg[1] = (byte)green;
                        dataPtrImg[2] = (byte)red;

                        // at th end of the line advance the pointer by the aligment bytes (padding)
                        dataPtrImg = dataPtrImg + nChan + padding;
                        dataPtrimgCopy = dataPtrimgCopy + nChan + padding;
                    }

                    // canto inferior esquerdo
                    blue = ((matrix[0, 0] * (dataPtrimgCopy - m.widthStep)[0]
                        + matrix[1, 0] * (dataPtrimgCopy - m.widthStep)[0]
                        + matrix[2, 0] * (dataPtrimgCopy + nChan - m.widthStep)[0]
                        + matrix[0, 1] * dataPtrimgCopy[0]
                        + matrix[1, 1] * dataPtrimgCopy[0]
                        + matrix[2, 1] * (dataPtrimgCopy + nChan)[0]
                        + matrix[0, 2] * dataPtrimgCopy[0]
                        + matrix[1, 2] * dataPtrimgCopy[0]
                        + matrix[2, 2] * (dataPtrimgCopy + nChan)[0]) / matrixWeight);
                    green = ((matrix[0, 0] * (dataPtrimgCopy - m.widthStep)[1]
                        + matrix[1, 0] * (dataPtrimgCopy - m.widthStep)[1]
                        + matrix[2, 0] * (dataPtrimgCopy + nChan - m.widthStep)[1]
                        + matrix[0, 1] * dataPtrimgCopy[1]
                        + matrix[1, 1] * dataPtrimgCopy[1]
                        + matrix[2, 1] * (dataPtrimgCopy + nChan)[1]
                        + matrix[0, 2] * dataPtrimgCopy[1]
                        + matrix[1, 2] * dataPtrimgCopy[1]
                        + matrix[2, 2] * (dataPtrimgCopy + nChan)[1]) / matrixWeight);
                    red = ((matrix[0, 0] * (dataPtrimgCopy - m.widthStep)[2]
                        + matrix[1, 0] * (dataPtrimgCopy - m.widthStep)[2]
                        + matrix[2, 0] * (dataPtrimgCopy + nChan - m.widthStep)[2]
                        + matrix[0, 1] * dataPtrimgCopy[2]
                        + matrix[1, 1] * dataPtrimgCopy[2]
                        + matrix[2, 1] * (dataPtrimgCopy + nChan)[2]
                        + matrix[0, 2] * dataPtrimgCopy[2]
                        + matrix[1, 2] * dataPtrimgCopy[2]
                        + matrix[2, 2] * (dataPtrimgCopy + nChan)[2]) / matrixWeight);

                    blue = (int)Math.Round(blue);
                    green = (int)Math.Round(green);
                    red = (int)Math.Round(red);
                    if (blue >= 255)
                        blue = 255;
                    if (green >= 255)
                        green = 255;
                    if (red >= 255)
                        red = 255;
                    if (blue <= 0)
                        blue = 0;
                    if (green <= 0)
                        green = 0;
                    if (red <= 0)
                        red = 0;

                    dataPtrImg[0] = (byte)blue;
                    dataPtrImg[1] = (byte)green;
                    dataPtrImg[2] = (byte)red;

                    // advance the pointer to the next pixel
                    dataPtrImg += nChan;
                    dataPtrimgCopy += nChan;

                    // linha de baixo
                    for (xd = 1; xd < last_w; xd++)
                    {
                        blue = ((matrix[0, 0] * (dataPtrimgCopy - nChan - m.widthStep)[0]
                            + matrix[1, 0] * (dataPtrimgCopy - m.widthStep)[0]
                            + matrix[2, 0] * (dataPtrimgCopy + nChan - m.widthStep)[0]
                            + matrix[0, 1] * (dataPtrimgCopy - nChan)[0]
                            + matrix[1, 1] * dataPtrimgCopy[0]
                            + matrix[2, 1] * (dataPtrimgCopy + nChan)[0]
                            + matrix[0, 2] * (dataPtrimgCopy - nChan)[0]
                            + matrix[1, 2] * dataPtrimgCopy[0]
                            + matrix[2, 2] * (dataPtrimgCopy + nChan)[0]) / matrixWeight);
                        green = ((matrix[0, 0] * (dataPtrimgCopy - nChan - m.widthStep)[1]
                            + matrix[1, 0] * (dataPtrimgCopy - m.widthStep)[1]
                            + matrix[2, 0] * (dataPtrimgCopy + nChan - m.widthStep)[1]
                            + matrix[0, 1] * (dataPtrimgCopy - nChan)[1]
                            + matrix[1, 1] * dataPtrimgCopy[1]
                            + matrix[2, 1] * (dataPtrimgCopy + nChan)[1]
                            + matrix[0, 2] * (dataPtrimgCopy - nChan)[1]
                            + matrix[1, 2] * dataPtrimgCopy[1]
                            + matrix[2, 2] * (dataPtrimgCopy + nChan)[1]) / matrixWeight);
                        red = ((matrix[0, 0] * (dataPtrimgCopy - nChan - m.widthStep)[2]
                            + matrix[1, 0] * (dataPtrimgCopy - m.widthStep)[2]
                            + matrix[2, 0] * (dataPtrimgCopy + nChan - m.widthStep)[2]
                            + matrix[0, 1] * (dataPtrimgCopy - nChan)[2]
                            + matrix[1, 1] * dataPtrimgCopy[2]
                            + matrix[2, 1] * (dataPtrimgCopy + nChan)[2]
                            + matrix[0, 2] * (dataPtrimgCopy - nChan)[2]
                            + matrix[1, 2] * dataPtrimgCopy[2]
                            + matrix[2, 2] * (dataPtrimgCopy + nChan)[2]) / matrixWeight);

                        blue = (int)Math.Round(blue);
                        green = (int)Math.Round(green);
                        red = (int)Math.Round(red);
                        if (blue >= 255)
                            blue = 255;
                        if (green >= 255)
                            green = 255;
                        if (red >= 255)
                            red = 255;
                        if (blue <= 0)
                            blue = 0;
                        if (green <= 0)
                            green = 0;
                        if (red <= 0)
                            red = 0;

                        dataPtrImg[0] = (byte)blue;
                        dataPtrImg[1] = (byte)green;
                        dataPtrImg[2] = (byte)red;

                        dataPtrImg += nChan;
                        dataPtrimgCopy += nChan;
                    }
                    // canto inferior direito
                    blue = ((matrix[0, 0] * (dataPtrimgCopy - nChan - m.widthStep)[0]
                        + matrix[1, 0] * (dataPtrimgCopy - m.widthStep)[0]
                        + matrix[2, 0] * (dataPtrimgCopy - m.widthStep)[0]
                        + matrix[0, 1] * (dataPtrimgCopy - nChan)[0]
                        + matrix[1, 1] * dataPtrimgCopy[0]
                        + matrix[2, 1] * dataPtrimgCopy[0]
                        + matrix[0, 2] * (dataPtrimgCopy - nChan)[0]
                        + matrix[1, 2] * dataPtrimgCopy[0]
                        + matrix[2, 2] * dataPtrimgCopy[0]) / matrixWeight);
                    green = ((matrix[0, 0] * (dataPtrimgCopy - nChan - m.widthStep)[1]
                        + matrix[1, 0] * (dataPtrimgCopy - m.widthStep)[1]
                        + matrix[2, 0] * (dataPtrimgCopy - m.widthStep)[1]
                        + matrix[0, 1] * (dataPtrimgCopy - nChan)[1]
                        + matrix[1, 1] * dataPtrimgCopy[1]
                        + matrix[2, 1] * dataPtrimgCopy[1]
                        + matrix[0, 2] * (dataPtrimgCopy - nChan)[1]
                        + matrix[1, 2] * dataPtrimgCopy[1]
                        + matrix[2, 2] * dataPtrimgCopy[1]) / matrixWeight);
                    red = ((matrix[0, 0] * (dataPtrimgCopy - nChan - m.widthStep)[2]
                        + matrix[1, 0] * (dataPtrimgCopy - m.widthStep)[2]
                        + matrix[2, 0] * (dataPtrimgCopy - m.widthStep)[2]
                        + matrix[0, 1] * (dataPtrimgCopy - nChan)[2]
                        + matrix[1, 1] * dataPtrimgCopy[2]
                        + matrix[2, 1] * dataPtrimgCopy[2]
                        + matrix[0, 2] * (dataPtrimgCopy - nChan)[2]
                        + matrix[1, 2] * dataPtrimgCopy[2]
                        + matrix[2, 2] * dataPtrimgCopy[2]) / matrixWeight);
                    blue = (int)Math.Round(blue);
                    green = (int)Math.Round(green);
                    red = (int)Math.Round(red);
                    if (blue >= 255)
                        blue = 255;
                    if (green >= 255)
                        green = 255;
                    if (red >= 255)
                        red = 255;
                    if (blue <= 0)
                        blue = 0;
                    if (green <= 0)
                        green = 0;
                    if (red <= 0)
                        red = 0;

                    dataPtrImg[0] = (byte)blue;
                    dataPtrImg[1] = (byte)green;
                    dataPtrImg[2] = (byte)red;

                    dataPtrImg += padding;
                    dataPtrimgCopy += padding;
                }
            }
        }

        //------------------------------------------(Sobel)------------------------------------------------------

        // OBG -> Finalizado BorderDiff(0,726)

        public static void Sobel(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy)
        {
            unsafe
            {
                MIplImage m = img.MIplImage; // imagem destino
                MIplImage mUndo = imgCopy.MIplImage; // imagem original

                byte* dataPtrimgCopy = (byte*)mUndo.imageData.ToPointer(); // Pointer to the original image 
                byte* dataPtrImg = (byte*)m.imageData.ToPointer(); // Pointer to the destany image
                int width = imgCopy.Width;
                int height = imgCopy.Height;
                int nChan = m.nChannels;
                int widthstep = m.widthStep;
                int padding = m.widthStep - m.nChannels * m.width; //alinhamento
                int xd, yd;
                int sx_blue, sx_red, sx_green, sy_blue, sy_red, sy_green;
                int last_h = height - 1;
                int last_w = width - 1;
                float blue;
                float green;
                float red;

                if (nChan == 3)
                {
                    //campo superior direito

                    sx_blue = (int)((3 * dataPtrimgCopy[0] + (dataPtrimgCopy + m.widthStep)[0])
                        - (3 * (dataPtrimgCopy + nChan)[0] + (dataPtrimgCopy + nChan + m.widthStep)[0]));

                    sx_green = (int)((3 * dataPtrimgCopy[1] + (dataPtrimgCopy + m.widthStep)[1])
                        - (3 * (dataPtrimgCopy + nChan)[1] + (dataPtrimgCopy + nChan + m.widthStep)[1]));

                    sx_red = (int)((3 * dataPtrimgCopy[2] + (dataPtrimgCopy + m.widthStep)[2])
                        - (3 * (dataPtrimgCopy + nChan)[2] + (dataPtrimgCopy + nChan + m.widthStep)[2]));

                    sy_blue = (int)((3 * dataPtrimgCopy[0] + (dataPtrimgCopy + nChan)[0])
                        - (3 * (dataPtrimgCopy + m.widthStep)[0] + (dataPtrimgCopy + nChan + m.widthStep)[0]));

                    sy_green = (int)((3 * dataPtrimgCopy[1] + (dataPtrimgCopy + nChan)[1])
                        - (3 * (dataPtrimgCopy + m.widthStep)[1] + (dataPtrimgCopy + nChan + m.widthStep)[1]));

                    sy_red = (int)((3 * dataPtrimgCopy[2] + (dataPtrimgCopy + nChan)[2])
                        - (3 * (dataPtrimgCopy + m.widthStep)[2] + (dataPtrimgCopy + nChan + m.widthStep)[2]));

                    blue = Math.Abs(sx_blue) + Math.Abs(sy_blue);
                    green = Math.Abs(sx_green) + Math.Abs(sy_green); ;
                    red = Math.Abs(sx_red) + Math.Abs(sy_red);

                    if (blue >= 255) blue = 255;
                    if (green >= 255) green = 255;
                    if (red >= 255) red = 255;
                    if (blue <= 0) blue = 0;
                    if (green <= 0) green = 0;
                    if (red <= 0) red = 0;

                    dataPtrImg[0] = (byte)blue;
                    dataPtrImg[1] = (byte)green;
                    dataPtrImg[2] = (byte)red;

                    dataPtrImg += nChan;
                    dataPtrimgCopy += nChan;


                }

                //linha de cima
                for (xd = 1; xd < last_w; xd++)
                {

                    sx_blue = (int)((3 * (dataPtrimgCopy + nChan)[0] + (dataPtrimgCopy + nChan + m.widthStep)[0])
                         - (3 * (dataPtrimgCopy + nChan)[0] + (dataPtrimgCopy + nChan + m.widthStep)[0]));

                    sx_green = (int)((3 * (dataPtrimgCopy - nChan)[1] + (dataPtrimgCopy - nChan + m.widthStep)[1])
                         - (3 * (dataPtrimgCopy + nChan)[1] + (dataPtrimgCopy + nChan + m.widthStep)[1]));


                    sx_red = (int)((3 * (dataPtrimgCopy - nChan)[2] + (dataPtrimgCopy - nChan + m.widthStep)[2])
                         - (3 * (dataPtrimgCopy + nChan)[2] + (dataPtrimgCopy + nChan + m.widthStep)[2]));


                    sy_blue = (int)(((dataPtrimgCopy - nChan)[0] + 2 * dataPtrimgCopy[0] + (dataPtrimgCopy + nChan)[0])
                        - ((dataPtrimgCopy - nChan + m.widthStep)[0] + 2 * (dataPtrimgCopy + m.widthStep)[0] + (dataPtrimgCopy + nChan + m.widthStep)[0]));

                    sy_green = (int)(((dataPtrimgCopy - nChan)[1] + 2 * dataPtrimgCopy[1] + (dataPtrimgCopy + nChan)[1])
                        - ((dataPtrimgCopy - nChan + m.widthStep)[1] + 2 * (dataPtrimgCopy + m.widthStep)[1] + (dataPtrimgCopy + nChan + m.widthStep)[1]));

                    sy_red = (int)(((dataPtrimgCopy - nChan)[2] + 2 * dataPtrimgCopy[2] + (dataPtrimgCopy + nChan)[2])
                        - ((dataPtrimgCopy - nChan + m.widthStep)[2] + 2 * (dataPtrimgCopy + m.widthStep)[2] + (dataPtrimgCopy + nChan + m.widthStep)[2]));

                    blue = Math.Abs(sx_blue) + Math.Abs(sy_blue);
                    green = Math.Abs(sx_green) + Math.Abs(sy_green); ;
                    red = Math.Abs(sx_red) + Math.Abs(sy_red);

                    if (blue >= 255) blue = 255;
                    if (green >= 255) green = 255;
                    if (red >= 255) red = 255;
                    if (blue <= 0) blue = 0;
                    if (green <= 0) green = 0;
                    if (red <= 0) red = 0;

                    dataPtrImg[0] = (byte)blue;
                    dataPtrImg[1] = (byte)green;
                    dataPtrImg[2] = (byte)red;

                    //advance the pointer to the next pixel
                    dataPtrImg += nChan;
                    dataPtrimgCopy += nChan;
                }

                //canto sup direito capaz de ser fora do if
                if (nChan == 3)
                {

                    sx_blue = (int)((3 * (dataPtrimgCopy - nChan)[0] + (dataPtrimgCopy - nChan + m.widthStep)[0])
                        - (3 * dataPtrimgCopy[0] + (dataPtrimgCopy + m.widthStep)[0]));

                    sx_green = (int)((3 * (dataPtrimgCopy - nChan)[1] + (dataPtrimgCopy - nChan + m.widthStep)[1])
                        - (3 * dataPtrimgCopy[1] + (dataPtrimgCopy + m.widthStep)[1]));

                    sx_red = (int)((3 * (dataPtrimgCopy - nChan)[2] + (dataPtrimgCopy - nChan + m.widthStep)[2])
                        - (3 * dataPtrimgCopy[2] + (dataPtrimgCopy + m.widthStep)[2]));

                    sy_blue = (int)((3 * dataPtrimgCopy[0]
                        + (dataPtrimgCopy - nChan)[0])) - (3 * (dataPtrimgCopy + m.widthStep)[0] + (dataPtrimgCopy - nChan + m.widthStep)[0]);

                    sy_green = (int)((3 * dataPtrimgCopy[1]
                        + (dataPtrimgCopy - nChan)[1])) - (3 * (dataPtrimgCopy + m.widthStep)[1] + (dataPtrimgCopy - nChan + m.widthStep)[1]);


                    sy_red = (int)((3 * dataPtrimgCopy[2]
                        + (dataPtrimgCopy - nChan)[2])) - (3 * (dataPtrimgCopy + m.widthStep)[2] + (dataPtrimgCopy - nChan + m.widthStep)[2]);

                    blue = Math.Abs(sx_blue) + Math.Abs(sy_blue);
                    green = Math.Abs(sx_green) + Math.Abs(sy_green); ;
                    red = Math.Abs(sx_red) + Math.Abs(sy_red);

                    if (blue >= 255) blue = 255;
                    if (green >= 255) green = 255;
                    if (red >= 255) red = 255;
                    if (blue <= 0) blue = 0;
                    if (green <= 0) green = 0;
                    if (red <= 0) red = 0;

                    dataPtrImg[0] = (byte)blue;
                    dataPtrImg[1] = (byte)green;
                    dataPtrImg[2] = (byte)red;

                    dataPtrImg = dataPtrImg + nChan + padding;
                    dataPtrimgCopy = dataPtrimgCopy + nChan + padding;

                }
                for (yd = 1; yd < last_h; yd++)
                {
                    //pixel esquerdo da linha
                    sx_blue = (int)(((dataPtrimgCopy - m.widthStep)[0] + 2 * dataPtrimgCopy[0] + (dataPtrimgCopy + m.widthStep)[0])
                         - ((dataPtrimgCopy + nChan - m.widthStep)[0] + 2 * (dataPtrimgCopy + nChan)[0] + (dataPtrimgCopy + nChan + m.widthStep)[0]));

                    sx_green = (int)(((dataPtrimgCopy - m.widthStep)[1] + 2 * dataPtrimgCopy[1] + (dataPtrimgCopy + m.widthStep)[1])
                         - ((dataPtrimgCopy + nChan - m.widthStep)[1] + 2 * (dataPtrimgCopy + nChan)[1] + (dataPtrimgCopy + nChan + m.widthStep)[1]));


                    sx_red = (int)(((dataPtrimgCopy - m.widthStep)[2] + 2 * dataPtrimgCopy[2] + (dataPtrimgCopy + m.widthStep)[2])
                         - ((dataPtrimgCopy + nChan - m.widthStep)[2] + 2 * (dataPtrimgCopy + nChan)[2] + (dataPtrimgCopy + nChan + m.widthStep)[2]));

                    sy_blue = (int)((3 * (dataPtrimgCopy + m.widthStep)[0] + (dataPtrimgCopy + nChan + m.widthStep)[0])
                        - (3 * (dataPtrimgCopy - m.widthStep)[0] + (dataPtrimgCopy + nChan - m.widthStep)[0]));

                    sy_green = (int)((3 * (dataPtrimgCopy + m.widthStep)[1] + (dataPtrimgCopy + nChan + m.widthStep)[1])
                        - (3 * (dataPtrimgCopy - m.widthStep)[1] + (dataPtrimgCopy + nChan - m.widthStep)[1]));

                    sy_red = (int)((3 * (dataPtrimgCopy + m.widthStep)[1] + (dataPtrimgCopy + nChan + m.widthStep)[1])
                        - (3 * (dataPtrimgCopy - m.widthStep)[1] + (dataPtrimgCopy + nChan - m.widthStep)[1]));

                    blue = Math.Abs(sx_blue) + Math.Abs(sy_blue);
                    green = Math.Abs(sx_green) + Math.Abs(sy_green); ;
                    red = Math.Abs(sx_red) + Math.Abs(sy_red);

                    if (blue >= 255) blue = 255;
                    if (green >= 255) green = 255;
                    if (red >= 255) red = 255;
                    if (blue <= 0) blue = 0;
                    if (green <= 0) green = 0;
                    if (red <= 0) red = 0;

                    dataPtrImg[0] = (byte)blue;
                    dataPtrImg[1] = (byte)green;
                    dataPtrImg[2] = (byte)red;

                    dataPtrImg += nChan;
                    dataPtrimgCopy += nChan;

                    for (int x = 1; x < last_w; x++)
                    {
                        sx_blue = (int)(((dataPtrimgCopy - nChan - m.widthStep)[0] + 2 * (dataPtrimgCopy - nChan)[0] + (dataPtrimgCopy - nChan + m.widthStep)[0])
                         - ((dataPtrimgCopy + nChan - m.widthStep)[0] + 2 * (dataPtrimgCopy + nChan)[0] + (dataPtrimgCopy + nChan + m.widthStep)[0]));

                        sx_green = (int)(((dataPtrimgCopy - nChan - m.widthStep)[1] + 2 * (dataPtrimgCopy - nChan)[1] + (dataPtrimgCopy - nChan + m.widthStep)[1])
                         - ((dataPtrimgCopy + nChan - m.widthStep)[1] + 2 * (dataPtrimgCopy + nChan)[1] + (dataPtrimgCopy + nChan + m.widthStep)[1]));


                        sx_red = (int)(((dataPtrimgCopy - nChan - m.widthStep)[2] + 2 * (dataPtrimgCopy - nChan)[2] + (dataPtrimgCopy - nChan + m.widthStep)[2])
                         - ((dataPtrimgCopy + nChan - m.widthStep)[2] + 2 * (dataPtrimgCopy + nChan)[2] + (dataPtrimgCopy + nChan + m.widthStep)[2]));

                        sy_blue = (int)(((dataPtrimgCopy - nChan + m.widthStep)[0] + 2 * (dataPtrimgCopy + m.widthStep)[0] + (dataPtrimgCopy + nChan + m.widthStep)[0])
                         - ((dataPtrimgCopy - nChan - m.widthStep)[0] + 2 * (dataPtrimgCopy - m.widthStep)[0] + (dataPtrimgCopy + nChan - m.widthStep)[0]));

                        sy_green = (int)(((dataPtrimgCopy - nChan + m.widthStep)[1] + 2 * (dataPtrimgCopy + m.widthStep)[1] + (dataPtrimgCopy + nChan + m.widthStep)[1])
                         - ((dataPtrimgCopy - nChan - m.widthStep)[1] + 2 * (dataPtrimgCopy - m.widthStep)[1] + (dataPtrimgCopy + nChan - m.widthStep)[1]));

                        sy_red = (int)(((dataPtrimgCopy - nChan + m.widthStep)[2] + 2 * (dataPtrimgCopy + m.widthStep)[2] + (dataPtrimgCopy + nChan + m.widthStep)[2])
                         - ((dataPtrimgCopy - nChan - m.widthStep)[2] + 2 * (dataPtrimgCopy - m.widthStep)[2] + (dataPtrimgCopy + nChan - m.widthStep)[2]));

                        blue = Math.Abs(sx_blue) + Math.Abs(sy_blue);
                        green = Math.Abs(sx_green) + Math.Abs(sy_green); ;
                        red = Math.Abs(sx_red) + Math.Abs(sy_red);

                        if (blue >= 255) blue = 255;
                        if (green >= 255) green = 255;
                        if (red >= 255) red = 255;
                        if (blue <= 0) blue = 0;
                        if (green <= 0) green = 0;
                        if (red <= 0) red = 0;

                        dataPtrImg[0] = (byte)blue;
                        dataPtrImg[1] = (byte)green;
                        dataPtrImg[2] = (byte)red;

                        //advance the pointer to the next pixel
                        dataPtrImg += nChan;
                        dataPtrimgCopy += nChan;

                    }

                    //pixel direito
                    sx_blue = (int)(((dataPtrimgCopy - m.widthStep - nChan)[0] + 2 * (dataPtrimgCopy - nChan)[0] + (dataPtrimgCopy - nChan + m.widthStep)[0])
                         - ((dataPtrimgCopy - m.widthStep)[0] + 2 * (dataPtrimgCopy)[0] + (dataPtrimgCopy + m.widthStep)[0]));

                    sx_green = (int)(((dataPtrimgCopy - m.widthStep - nChan)[1] + 2 * (dataPtrimgCopy - nChan)[1] + (dataPtrimgCopy - nChan + m.widthStep)[1])
                         - ((dataPtrimgCopy - m.widthStep)[1] + 2 * (dataPtrimgCopy)[1] + (dataPtrimgCopy + m.widthStep)[1]));


                    sx_red = (int)(((dataPtrimgCopy - m.widthStep - nChan)[2] + 2 * (dataPtrimgCopy - nChan)[2] + (dataPtrimgCopy - nChan + m.widthStep)[2])
                         - ((dataPtrimgCopy - m.widthStep)[2] + 2 * (dataPtrimgCopy)[2] + (dataPtrimgCopy + m.widthStep)[2]));

                    sy_blue = (int)(((dataPtrimgCopy + m.widthStep - nChan)[0] + 2 * (dataPtrimgCopy + m.widthStep)[0] + (dataPtrimgCopy + m.widthStep)[0])
                         - ((dataPtrimgCopy - nChan - m.widthStep)[0] + 2 * (dataPtrimgCopy - m.widthStep)[0] + (dataPtrimgCopy - m.widthStep)[0]));


                    sy_green = (int)(((dataPtrimgCopy + m.widthStep - nChan)[1] + 2 * (dataPtrimgCopy + m.widthStep)[1] + (dataPtrimgCopy + m.widthStep)[1])
                         - ((dataPtrimgCopy - nChan - m.widthStep)[1] + 2 * (dataPtrimgCopy - m.widthStep)[1] + (dataPtrimgCopy - m.widthStep)[1]));

                    sy_red = (int)(((dataPtrimgCopy + m.widthStep - nChan)[2] + 2 * (dataPtrimgCopy + m.widthStep)[2] + (dataPtrimgCopy + m.widthStep)[2])
                         - ((dataPtrimgCopy - nChan - m.widthStep)[2] + 2 * (dataPtrimgCopy - m.widthStep)[2] + (dataPtrimgCopy - m.widthStep)[2]));

                    blue = Math.Abs(sx_blue) + Math.Abs(sy_blue);
                    green = Math.Abs(sx_green) + Math.Abs(sy_green); ;
                    red = Math.Abs(sx_red) + Math.Abs(sy_red);

                    if (blue >= 255) blue = 255;
                    if (green >= 255) green = 255;
                    if (red >= 255) red = 255;
                    if (blue <= 0) blue = 0;
                    if (green <= 0) green = 0;
                    if (red <= 0) red = 0;

                    dataPtrImg[0] = (byte)blue;
                    dataPtrImg[1] = (byte)green;
                    dataPtrImg[2] = (byte)red;

                    dataPtrImg = dataPtrImg + nChan + padding;
                    dataPtrimgCopy = dataPtrimgCopy + nChan + padding;
                }



                //canto inferior esquerdo
                sx_blue = (int)(((dataPtrimgCopy - widthstep)[0] + 2 * dataPtrimgCopy[0] + (dataPtrimgCopy[0]))
                          - ((dataPtrimgCopy + nChan - widthstep)[0] + 2 * (dataPtrimgCopy + nChan)[0] + (dataPtrimgCopy + nChan)[0]));

                sx_green = (int)(((dataPtrimgCopy - widthstep)[1] + 2 * dataPtrimgCopy[1] + (dataPtrimgCopy[1]))
                          - ((dataPtrimgCopy + nChan - widthstep)[1] + 2 * (dataPtrimgCopy + nChan)[1] + (dataPtrimgCopy + nChan)[1]));


                sx_red = (int)(((dataPtrimgCopy - widthstep)[2] + 2 * dataPtrimgCopy[2] + (dataPtrimgCopy[2]))
                          - ((dataPtrimgCopy + nChan - widthstep)[2] + 2 * (dataPtrimgCopy + nChan)[2] + (dataPtrimgCopy + nChan)[2]));

                sy_blue = (int)(((dataPtrimgCopy - widthstep)[0] + 2 * (dataPtrimgCopy - widthstep)[0] + (dataPtrimgCopy + nChan - widthstep)[0])
                         - ((dataPtrimgCopy)[0] + 2 * (dataPtrimgCopy)[0] + (dataPtrimgCopy + nChan)[0]));

                sy_green = (int)(((dataPtrimgCopy - widthstep)[1] + 2 * (dataPtrimgCopy - widthstep)[1] + (dataPtrimgCopy + nChan - widthstep)[1])
                         - ((dataPtrimgCopy)[1] + 2 * (dataPtrimgCopy)[1] + (dataPtrimgCopy + nChan)[1]));

                sy_red = (int)(((dataPtrimgCopy - widthstep)[2] + 2 * (dataPtrimgCopy - widthstep)[2] + (dataPtrimgCopy + nChan - widthstep)[2])
                         - ((dataPtrimgCopy)[2] + 2 * (dataPtrimgCopy)[2] + (dataPtrimgCopy + nChan)[2]));

                blue = Math.Abs(sx_blue) + Math.Abs(sy_blue);
                green = Math.Abs(sx_green) + Math.Abs(sy_green); ;
                red = Math.Abs(sx_red) + Math.Abs(sy_red);

                if (blue >= 255) blue = 255;
                if (green >= 255) green = 255;
                if (red >= 255) red = 255;
                if (blue <= 0) blue = 0;
                if (green <= 0) green = 0;
                if (red <= 0) red = 0;

                dataPtrImg[0] = (byte)blue;
                dataPtrImg[1] = (byte)green;
                dataPtrImg[2] = (byte)red;

                dataPtrImg += nChan;
                dataPtrimgCopy += nChan;

                //linha de baixo
                for (int i = 0; i < last_w; i++)
                {
                    sx_blue = (int)(((dataPtrimgCopy - widthstep - nChan)[0] + 2 * (dataPtrimgCopy - nChan)[0] + (dataPtrimgCopy - nChan)[0])
                         - ((dataPtrimgCopy + nChan - widthstep)[0] + 2 * (dataPtrimgCopy + nChan)[0] + (dataPtrimgCopy + nChan)[0]));

                    sx_green = (int)(((dataPtrimgCopy - widthstep - nChan)[1] + 2 * (dataPtrimgCopy - nChan)[1] + (dataPtrimgCopy - nChan)[1])
                         - ((dataPtrimgCopy + nChan - widthstep)[1] + 2 * (dataPtrimgCopy + nChan)[1] + (dataPtrimgCopy + nChan)[1]));


                    sx_red = (int)(((dataPtrimgCopy - widthstep - nChan)[2] + 2 * (dataPtrimgCopy - nChan)[2] + (dataPtrimgCopy - nChan)[2])
                         - ((dataPtrimgCopy + nChan - widthstep)[2] + 2 * (dataPtrimgCopy + nChan)[2] + (dataPtrimgCopy + nChan)[2]));

                    sy_blue = (int)(((dataPtrimgCopy - widthstep - nChan)[0] + 2 * (dataPtrimgCopy - widthstep)[0] + (dataPtrimgCopy - widthstep + nChan)[0])
                         - ((dataPtrimgCopy - nChan)[0] + 2 * (dataPtrimgCopy)[0] + (dataPtrimgCopy + nChan)[0]));

                    sy_green = (int)(((dataPtrimgCopy - widthstep - nChan)[1] + 2 * (dataPtrimgCopy - widthstep)[1] + (dataPtrimgCopy - widthstep + nChan)[1])
                         - ((dataPtrimgCopy - nChan)[1] + 2 * (dataPtrimgCopy)[1] + (dataPtrimgCopy + nChan)[1]));

                    sy_red = (int)(((dataPtrimgCopy - widthstep - nChan)[2] + 2 * (dataPtrimgCopy - widthstep)[2] + (dataPtrimgCopy - widthstep + nChan)[2])
                         - ((dataPtrimgCopy - nChan)[2] + 2 * (dataPtrimgCopy)[2] + (dataPtrimgCopy + nChan)[2]));

                    blue = Math.Abs(sx_blue) + Math.Abs(sy_blue);
                    green = Math.Abs(sx_green) + Math.Abs(sy_green); ;
                    red = Math.Abs(sx_red) + Math.Abs(sy_red);

                    if (blue >= 255) blue = 255;
                    if (green >= 255) green = 255;
                    if (red >= 255) red = 255;
                    if (blue <= 0) blue = 0;
                    if (green <= 0) green = 0;
                    if (red <= 0) red = 0;

                    dataPtrImg[0] = (byte)blue;
                    dataPtrImg[1] = (byte)green;
                    dataPtrImg[2] = (byte)red;

                    dataPtrImg += nChan;
                    dataPtrimgCopy += nChan;

                }
                //conto inf direito
                sx_blue = (int)(((dataPtrimgCopy - widthstep - nChan)[0] + 2 * (dataPtrimgCopy - nChan)[0] + (dataPtrimgCopy - nChan)[0])
                         - ((dataPtrimgCopy - widthstep)[0] + 2 * (dataPtrimgCopy)[0] + (dataPtrimgCopy)[0]));

                sx_green = (int)(((dataPtrimgCopy - widthstep - nChan)[1] + 2 * (dataPtrimgCopy - nChan)[1] + (dataPtrimgCopy - nChan)[1])
                         - ((dataPtrimgCopy - widthstep)[1] + 2 * (dataPtrimgCopy)[1] + (dataPtrimgCopy)[1]));


                sx_red = (int)(((dataPtrimgCopy - widthstep - nChan)[2] + 2 * (dataPtrimgCopy - nChan)[2] + (dataPtrimgCopy - nChan)[2])
                         - ((dataPtrimgCopy - widthstep)[2] + 2 * (dataPtrimgCopy)[2] + (dataPtrimgCopy)[2]));

                sy_blue = (int)(((dataPtrimgCopy - widthstep - nChan)[0] + 2 * (dataPtrimgCopy - widthstep)[0] + (dataPtrimgCopy - widthstep)[0])
                         - ((dataPtrimgCopy - nChan)[0] + 2 * (dataPtrimgCopy)[0] + (dataPtrimgCopy)[0]));

                sy_green = (int)(((dataPtrimgCopy - widthstep - nChan)[1] + 2 * (dataPtrimgCopy - widthstep)[1] + (dataPtrimgCopy - widthstep)[1])
                         - ((dataPtrimgCopy - nChan)[1] + 2 * (dataPtrimgCopy)[1] + (dataPtrimgCopy)[1]));

                sy_red = (int)(((dataPtrimgCopy - widthstep - nChan)[2] + 2 * (dataPtrimgCopy - widthstep)[2] + (dataPtrimgCopy - widthstep)[2])
                         - ((dataPtrimgCopy - nChan)[2] + 2 * (dataPtrimgCopy)[2] + (dataPtrimgCopy)[2]));

                blue = Math.Abs(sx_blue) + Math.Abs(sy_blue);
                green = Math.Abs(sx_green) + Math.Abs(sy_green); ;
                red = Math.Abs(sx_red) + Math.Abs(sy_red);

                if (blue >= 255) blue = 255;
                if (green >= 255) green = 255;
                if (red >= 255) red = 255;
                if (blue <= 0) blue = 0;
                if (green <= 0) green = 0;
                if (red <= 0) red = 0;

                dataPtrImg[0] = (byte)blue;
                dataPtrImg[1] = (byte)green;
                dataPtrImg[2] = (byte)red;

                dataPtrImg += nChan;
                dataPtrimgCopy += nChan;

            }
        }

        //----------------------------------------(Diferetiation)------------------------------------------------------

        // OBG -> Finalizado

        public static void Diferentiation(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                MIplImage mUndo = imgCopy.MIplImage;
                byte* dataPtrimgCopy = (byte*)mUndo.imageData.ToPointer(); // Pointer to the original image 
                byte* dataPtrImg = (byte*)m.imageData.ToPointer(); // Pointer to the destination image

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels;
                int widthStep = m.widthStep;
                int padding = m.widthStep - m.nChannels * m.width;
                int xd, yd;
                int last_h = height - 1;
                int last_w = width - 1;
                int blue, red, green;

                // Iterating over the image
                for (yd = 0; yd < last_h; yd++)
                {
                    for (xd = 0; xd < last_w; xd++)
                    {
                        // Calculate differences in BGR channels between neighboring pixels
                        blue = (int)(Math.Abs(dataPtrimgCopy[0] - (dataPtrimgCopy + nChan)[0]) + Math.Abs(dataPtrimgCopy[0] - (dataPtrimgCopy + widthStep)[0]));
                        green = (int)(Math.Abs(dataPtrimgCopy[1] - (dataPtrimgCopy + nChan)[1]) + Math.Abs(dataPtrimgCopy[1] - (dataPtrimgCopy + widthStep)[1]));
                        red = (int)(Math.Abs(dataPtrimgCopy[2] - (dataPtrimgCopy + nChan)[2]) + Math.Abs(dataPtrimgCopy[2] - (dataPtrimgCopy + widthStep)[2]));

                        // Clamping values to ensure they are within [0, 255]
                        blue = Math.Max(0, Math.Min(255, blue));
                        green = Math.Max(0, Math.Min(255, green));
                        red = Math.Max(0, Math.Min(255, red));

                        // Assigning values to the pixel in the destination image
                        dataPtrImg[0] = (byte)blue;
                        dataPtrImg[1] = (byte)green;
                        dataPtrImg[2] = (byte)red;

                        // Advancing pointers to the next pixels
                        dataPtrImg += nChan;
                        dataPtrimgCopy += nChan;
                    }


                    // Calculate differences in BGR channels between neighboring pixels
                    blue = (int)(Math.Abs(dataPtrimgCopy[0] - (dataPtrimgCopy + widthStep)[0]));
                    green = (int)(Math.Abs(dataPtrimgCopy[1] - (dataPtrimgCopy + widthStep)[1]));
                    red = (int)(Math.Abs(dataPtrimgCopy[2] - (dataPtrimgCopy + widthStep)[2]));

                    // Clamping values to ensure they are within [0, 255]
                    blue = Math.Max(0, Math.Min(255, blue));
                    green = Math.Max(0, Math.Min(255, green));
                    red = Math.Max(0, Math.Min(255, red));

                    // Assigning values to the pixel in the destination image
                    dataPtrImg[0] = (byte)blue;
                    dataPtrImg[1] = (byte)green;
                    dataPtrImg[2] = (byte)red;



                    // Updating pointers for the next row
                    dataPtrImg += padding + nChan;
                    dataPtrimgCopy += padding + nChan;
                }

                // Processing the last row
                for (int x = 0; x < last_w; x++)
                {
                    // Calculate differences in BGR channels between neighboring pixels
                    blue = (int)(Math.Abs(dataPtrimgCopy[0] - (dataPtrimgCopy + nChan)[0]));
                    green = (int)(Math.Abs(dataPtrimgCopy[1] - (dataPtrimgCopy + nChan)[1]));
                    red = (int)(Math.Abs(dataPtrimgCopy[2] - (dataPtrimgCopy + nChan)[2]));

                    // Clamping values to ensure they are within [0, 255]
                    blue = Math.Max(0, Math.Min(255, blue));
                    green = Math.Max(0, Math.Min(255, green));
                    red = Math.Max(0, Math.Min(255, red));

                    // Assigning values to the pixel in the destination image
                    dataPtrImg[0] = (byte)blue;
                    dataPtrImg[1] = (byte)green;
                    dataPtrImg[2] = (byte)red;

                    // Advancing pointers to the next pixels
                    dataPtrImg += nChan;
                    dataPtrimgCopy += nChan;
                }

                blue = 0;
                green = 0;
                red = 0;

                blue = Math.Max(0, Math.Min(255, blue));
                green = Math.Max(0, Math.Min(255, green));
                red = Math.Max(0, Math.Min(255, red));

                // Assigning values to the pixel in the destination image
                dataPtrImg[0] = (byte)blue;
                dataPtrImg[1] = (byte)green;
                dataPtrImg[2] = (byte)red;


            }
        }


        //------------------------------------------(Median)------------------------------------------------------

        // OBG ->  Finalizado CoreDiff(9.491) BorderDiff(2.073)

        public static void Median(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy)
        {
            unsafe
            {

                MIplImage m = img.MIplImage;
                MIplImage mUndo = imgCopy.MIplImage;

                byte* dataPtrimgCopy = (byte*)mUndo.imageData.ToPointer();
                byte* dataPtrimg = (byte*)m.imageData.ToPointer();


                int blue, green, red;

                int width = img.Width;
                int height = img.Height;
                int nChan = mUndo.nChannels; // number of channels - 3
                int widthstep = mUndo.widthStep;
                int padding = mUndo.widthStep - mUndo.nChannels * mUndo.width; // alinhament bytes (padding)
                int xd, yd;
                int sx_blue, sx_green, sx_red, sy_blue, sy_green, sy_red;


                int last_h = height - 1;
                int last_w = width - 1;


                int[] p = new int[9];
                int less_val = 0;

                p[0] = 0;
                p[0] += (int)(Math.Abs(dataPtrimgCopy[0] - (dataPtrimgCopy + nChan)[0])
                    + Math.Abs(dataPtrimgCopy[1] - (dataPtrimgCopy + nChan)[1]) + Math.Abs(dataPtrimgCopy[2] - (dataPtrimgCopy + nChan)[2])) * 2;
                p[0] += (int)(Math.Abs(dataPtrimgCopy[0] - (dataPtrimgCopy + m.widthStep)[0])
                    + Math.Abs(dataPtrimgCopy[1] - (dataPtrimgCopy + m.widthStep)[1]) + Math.Abs(dataPtrimgCopy[2] - (dataPtrimgCopy + m.widthStep)[2])) * 2;
                p[0] += (int)(Math.Abs(dataPtrimgCopy[0] - (dataPtrimgCopy + nChan + m.widthStep)[0])
                   + Math.Abs(dataPtrimgCopy[1] - (dataPtrimgCopy + nChan + m.widthStep)[1]) + Math.Abs(dataPtrimgCopy[2] - (dataPtrimgCopy + nChan + m.widthStep)[2]));

                p[1] = p[3] = p[4] = p[0];


                p[2] = 0;

                p[2] += (int)(Math.Abs((dataPtrimgCopy + nChan)[0] - dataPtrimgCopy[0])
                    + Math.Abs((dataPtrimgCopy + nChan)[1] - dataPtrimgCopy[1]) + Math.Abs((dataPtrimgCopy + nChan)[2] - dataPtrimgCopy[2])) * 4;

                p[2] += (int)(Math.Abs((dataPtrimgCopy + nChan)[0] - (dataPtrimgCopy + m.widthStep)[0]) +
                    Math.Abs((dataPtrimgCopy + nChan)[1] - (dataPtrimgCopy + m.widthStep)[1])
                    + Math.Abs((dataPtrimgCopy + nChan)[2] - (dataPtrimgCopy + m.widthStep)[2])) * 2;

                p[2] += (int)(Math.Abs((dataPtrimgCopy + nChan)[0] - (dataPtrimgCopy + nChan + m.widthStep)[0]) +
                    Math.Abs((dataPtrimgCopy + nChan)[1] - (dataPtrimgCopy + nChan + m.widthStep)[1])
                    + Math.Abs((dataPtrimgCopy + nChan)[2] - (dataPtrimgCopy + nChan + m.widthStep)[2]));

                p[5] = p[2];

                p[6] = 0;
                p[6] += (int)(Math.Abs((dataPtrimgCopy + m.widthStep)[0] - dataPtrimgCopy[0])
                    + Math.Abs((dataPtrimgCopy + m.widthStep)[1] - dataPtrimgCopy[1]) + Math.Abs((dataPtrimgCopy + m.widthStep)[2] - dataPtrimgCopy[2])) * 4;
                p[6] += (int)(Math.Abs((dataPtrimgCopy + m.widthStep)[0] - (dataPtrimgCopy + nChan)[0])
                    + Math.Abs((dataPtrimgCopy + m.widthStep)[1] - (dataPtrimgCopy + nChan)[1]) + Math.Abs((dataPtrimgCopy + m.widthStep)[2] - (dataPtrimgCopy + nChan)[2])) * 2;

                p[6] += (int)(Math.Abs((dataPtrimgCopy + m.widthStep)[0] - (dataPtrimgCopy + nChan + m.widthStep)[0])
                    + Math.Abs((dataPtrimgCopy + m.widthStep)[1] - (dataPtrimgCopy + nChan + m.widthStep)[1]) + Math.Abs((dataPtrimgCopy + m.widthStep)[2] - (dataPtrimgCopy + nChan + m.widthStep)[2]));



                p[7] = p[6];



                p[8] = 0;
                p[8] += (int)(Math.Abs((dataPtrimgCopy + nChan + m.widthStep)[0] - dataPtrimgCopy[0])
                    + Math.Abs((dataPtrimgCopy + nChan + m.widthStep)[1] - dataPtrimgCopy[1])
                    + Math.Abs((dataPtrimgCopy + nChan + m.widthStep)[2] - dataPtrimgCopy[2])) * 4;
                p[8] += (int)(Math.Abs((dataPtrimgCopy + nChan + m.widthStep)[0] - (dataPtrimgCopy + nChan)[0])
                    + Math.Abs((dataPtrimgCopy + nChan + m.widthStep)[1] - (dataPtrimgCopy + nChan)[1])
                    + Math.Abs((dataPtrimgCopy + nChan + m.widthStep)[2] - (dataPtrimgCopy + nChan)[2])) * 2;

                p[8] += (int)(Math.Abs((dataPtrimgCopy + nChan + m.widthStep)[0] - (dataPtrimgCopy + m.widthStep)[0])
                   + Math.Abs((dataPtrimgCopy + nChan + m.widthStep)[1] - (dataPtrimgCopy + m.widthStep)[1])
                   + Math.Abs((dataPtrimgCopy + nChan + m.widthStep)[2] - (dataPtrimgCopy + m.widthStep)[2])) * 2;

                less_val = Array.IndexOf(p, p.Min());

                switch (less_val)
                {
                    case 0:
                    case 1:
                    case 3:
                    case 4:
                        dataPtrimg[0] = dataPtrimgCopy[0];
                        dataPtrimg[1] = dataPtrimgCopy[1];
                        dataPtrimg[2] = dataPtrimgCopy[2];
                        break;
                    case 2:
                    case 5:
                        dataPtrimg[0] = (dataPtrimgCopy + nChan)[0];
                        dataPtrimg[1] = (dataPtrimgCopy + nChan)[1];
                        dataPtrimg[2] = (dataPtrimgCopy + nChan)[2];
                        break;
                    case 6:
                    case 7:
                        dataPtrimg[0] = (dataPtrimgCopy + m.widthStep)[0];
                        dataPtrimg[1] = (dataPtrimgCopy + m.widthStep)[1];
                        dataPtrimg[2] = (dataPtrimgCopy + m.widthStep)[2];
                        break;
                    case 8:
                        dataPtrimg[0] = (dataPtrimgCopy + m.widthStep + nChan)[0];
                        dataPtrimg[1] = (dataPtrimgCopy + m.widthStep + nChan)[1];
                        dataPtrimg[2] = (dataPtrimgCopy + m.widthStep + nChan)[2];
                        break;

                }

                dataPtrimg += nChan;
                dataPtrimgCopy += nChan;

                //Linha de Cima

                for (int x = 1; x < last_w; x++)
                {


                    p[0] = 0;
                    p[0] += (int)(Math.Abs((dataPtrimgCopy - nChan)[0] - (dataPtrimgCopy + nChan)[0])
                        + Math.Abs((dataPtrimgCopy - nChan)[1] - (dataPtrimgCopy + nChan)[1])
                        + Math.Abs((dataPtrimgCopy - nChan)[2] - (dataPtrimgCopy + nChan)[2])) * 2;
                    p[0] += (int)(Math.Abs((dataPtrimgCopy - nChan)[0] - dataPtrimgCopy[0])
                        + Math.Abs((dataPtrimgCopy - nChan)[1] - dataPtrimgCopy[1])
                        + Math.Abs((dataPtrimgCopy - nChan)[2] - dataPtrimgCopy[2])) * 2;
                    p[0] += (int)(Math.Abs((dataPtrimgCopy - nChan)[0] - (dataPtrimgCopy + m.widthStep)[0])
                        + Math.Abs((dataPtrimgCopy - nChan)[1] - (dataPtrimgCopy + m.widthStep)[1])
                        + Math.Abs((dataPtrimgCopy - nChan)[2] - (dataPtrimgCopy + m.widthStep)[2]));
                    p[0] += (int)(Math.Abs((dataPtrimgCopy - nChan)[0] - (dataPtrimgCopy - nChan + m.widthStep)[0])
                        + Math.Abs((dataPtrimgCopy - nChan)[1] - (dataPtrimgCopy - nChan + m.widthStep)[1])
                        + Math.Abs((dataPtrimgCopy - nChan)[2] - (dataPtrimgCopy - nChan + m.widthStep)[2]));
                    p[0] += (int)(Math.Abs((dataPtrimgCopy - nChan)[0] - (dataPtrimgCopy + nChan + m.widthStep)[0])
                        + Math.Abs((dataPtrimgCopy - nChan)[1] - (dataPtrimgCopy + nChan + m.widthStep)[1])
                        + Math.Abs((dataPtrimgCopy - nChan)[2] - (dataPtrimgCopy + nChan + m.widthStep)[2]));




                    p[3] = p[0];

                    p[1] = 0;
                    p[1] += (int)(Math.Abs(dataPtrimgCopy[0] - (dataPtrimgCopy + nChan)[0])
                    + Math.Abs(dataPtrimgCopy[1] - (dataPtrimgCopy + nChan)[1])
                    + Math.Abs(dataPtrimgCopy[2] - (dataPtrimgCopy + nChan)[2])) * 2;

                    p[1] += (int)(Math.Abs(dataPtrimgCopy[0] - (dataPtrimgCopy - nChan)[0])
                    + Math.Abs(dataPtrimgCopy[1] - (dataPtrimgCopy - nChan)[1])
                    + Math.Abs(dataPtrimgCopy[2] - (dataPtrimgCopy - nChan)[2])) * 2;

                    p[1] += (int)(Math.Abs(dataPtrimgCopy[0] - (dataPtrimgCopy + m.widthStep)[0])
                        + Math.Abs(dataPtrimgCopy[1] - (dataPtrimgCopy + m.widthStep)[1])
                        + Math.Abs(dataPtrimgCopy[2] - (dataPtrimgCopy + m.widthStep)[2]));

                    p[1] += (int)(Math.Abs(dataPtrimgCopy[0] - (dataPtrimgCopy + m.widthStep + nChan)[0])
                        + Math.Abs(dataPtrimgCopy[1] - (dataPtrimgCopy + m.widthStep + nChan)[1])
                        + Math.Abs(dataPtrimgCopy[2] - (dataPtrimgCopy + m.widthStep + nChan)[2]));


                    p[1] += (int)(Math.Abs(dataPtrimgCopy[0] - (dataPtrimgCopy + m.widthStep - nChan)[0])
                        + Math.Abs(dataPtrimgCopy[1] - (dataPtrimgCopy + m.widthStep - nChan)[1])
                        + Math.Abs(dataPtrimgCopy[2] - (dataPtrimgCopy + m.widthStep - nChan)[2]));

                    p[4] = p[1];

                    p[2] = 0;
                    p[2] += (int)(Math.Abs((dataPtrimgCopy + nChan)[0] - dataPtrimgCopy[0])
                        + Math.Abs((dataPtrimgCopy + nChan)[1] - dataPtrimgCopy[1])
                        + Math.Abs((dataPtrimgCopy + nChan)[2] - dataPtrimgCopy[2])) * 2;

                    p[2] += (int)(Math.Abs((dataPtrimgCopy + nChan)[0] - (dataPtrimgCopy - nChan)[0])
                       + Math.Abs((dataPtrimgCopy + nChan)[1] - (dataPtrimgCopy - nChan)[1])
                       + Math.Abs((dataPtrimgCopy + nChan)[2] - (dataPtrimgCopy - nChan)[2])) * 2;

                    p[2] += (int)(Math.Abs((dataPtrimgCopy + nChan)[0] - (dataPtrimgCopy + m.widthStep)[0])
                       + Math.Abs((dataPtrimgCopy + nChan)[1] - (dataPtrimgCopy + m.widthStep)[1])
                       + Math.Abs((dataPtrimgCopy + nChan)[2] - (dataPtrimgCopy + m.widthStep)[2]));


                    p[2] += (int)(Math.Abs((dataPtrimgCopy + nChan)[0] - (dataPtrimgCopy - nChan + m.widthStep)[0])
                       + Math.Abs((dataPtrimgCopy + nChan)[1] - (dataPtrimgCopy - nChan + m.widthStep)[1])
                       + Math.Abs((dataPtrimgCopy + nChan)[2] - (dataPtrimgCopy - nChan + m.widthStep)[2]));

                    p[2] += (int)(Math.Abs((dataPtrimgCopy + nChan)[0] - (dataPtrimgCopy + m.widthStep - nChan)[0])
                        + Math.Abs((dataPtrimgCopy + nChan)[1] - (dataPtrimgCopy + m.widthStep - nChan)[1])
                        + Math.Abs((dataPtrimgCopy + nChan)[2] - (dataPtrimgCopy + m.widthStep - nChan)[2]));

                    p[2] += (int)(Math.Abs((dataPtrimgCopy + nChan)[0] - (dataPtrimgCopy + m.widthStep + nChan)[0])
                        + Math.Abs((dataPtrimgCopy + nChan)[1] - (dataPtrimgCopy + m.widthStep + nChan)[1])
                        + Math.Abs((dataPtrimgCopy + nChan)[2] - (dataPtrimgCopy + m.widthStep + nChan)[2]));

                    p[5] = p[2];

                    p[6] = 0;

                    p[6] += (int)(Math.Abs((dataPtrimgCopy - nChan + m.widthStep)[0] - (dataPtrimgCopy + nChan)[0])
                        + Math.Abs((dataPtrimgCopy - nChan + m.widthStep)[1] - (dataPtrimgCopy + nChan)[1])
                        + Math.Abs((dataPtrimgCopy - nChan + m.widthStep)[2] - (dataPtrimgCopy + nChan)[2]));
                    p[6] += (int)(Math.Abs((dataPtrimgCopy - nChan + m.widthStep)[0] - (dataPtrimgCopy - nChan)[0])
                        + Math.Abs((dataPtrimgCopy - nChan + m.widthStep)[1] - (dataPtrimgCopy - nChan)[1])
                        + Math.Abs((dataPtrimgCopy - nChan + m.widthStep)[2] - (dataPtrimgCopy - nChan)[2]));
                    p[6] += (int)(Math.Abs((dataPtrimgCopy - nChan + m.widthStep)[0] - (dataPtrimgCopy + m.widthStep)[0])
                        + Math.Abs((dataPtrimgCopy - nChan + m.widthStep)[1] - (dataPtrimgCopy + m.widthStep)[1])
                        + Math.Abs((dataPtrimgCopy - nChan + m.widthStep)[2] - (dataPtrimgCopy + m.widthStep)[2]));
                    p[6] += (int)(Math.Abs((dataPtrimgCopy - nChan + m.widthStep)[0] - dataPtrimgCopy[0])
                        + Math.Abs((dataPtrimgCopy - nChan + m.widthStep)[1] - dataPtrimgCopy[1])
                        + Math.Abs((dataPtrimgCopy - nChan + m.widthStep)[2] - dataPtrimgCopy[2])) * 2;
                    p[6] += (int)(Math.Abs((dataPtrimgCopy - nChan + m.widthStep)[0] - (dataPtrimgCopy + nChan + m.widthStep)[0])
                        + Math.Abs((dataPtrimgCopy - nChan + m.widthStep)[1] - (dataPtrimgCopy + nChan + m.widthStep)[1])
                        + Math.Abs((dataPtrimgCopy - nChan + m.widthStep)[2] - (dataPtrimgCopy + nChan + m.widthStep)[2]));



                    p[7] = 0;
                    p[7] += (int)(Math.Abs((dataPtrimgCopy + m.widthStep)[0] - (dataPtrimgCopy + nChan)[0])
                        + Math.Abs((dataPtrimgCopy + m.widthStep)[1] - (dataPtrimgCopy + nChan)[1])
                        + Math.Abs((dataPtrimgCopy + m.widthStep)[2] - (dataPtrimgCopy + nChan)[2])) * 2;

                    p[7] += (int)(Math.Abs((dataPtrimgCopy + m.widthStep)[0] - (dataPtrimgCopy - nChan)[0])
                        + Math.Abs((dataPtrimgCopy + m.widthStep)[1] - (dataPtrimgCopy - nChan)[1])
                        + Math.Abs((dataPtrimgCopy + m.widthStep)[2] - (dataPtrimgCopy - nChan)[2])) * 2;

                    p[7] += (int)(Math.Abs((dataPtrimgCopy + m.widthStep)[0] - dataPtrimgCopy[0])
                        + Math.Abs((dataPtrimgCopy + m.widthStep)[1] - dataPtrimgCopy[1])
                        + Math.Abs((dataPtrimgCopy + m.widthStep)[2] - dataPtrimgCopy[2]));

                    p[7] += (int)(Math.Abs((dataPtrimgCopy + m.widthStep)[0] - (dataPtrimgCopy - nChan + m.widthStep)[1])
                        + Math.Abs((dataPtrimgCopy + m.widthStep)[1] - (dataPtrimgCopy - nChan + m.widthStep)[1])
                        + Math.Abs((dataPtrimgCopy + m.widthStep)[2] - (dataPtrimgCopy - nChan + m.widthStep)[1]));

                    p[7] += (int)(Math.Abs((dataPtrimgCopy + m.widthStep)[0] - (dataPtrimgCopy + nChan + m.widthStep)[1])
                       + Math.Abs((dataPtrimgCopy + m.widthStep)[1] - (dataPtrimgCopy + nChan + m.widthStep)[1])
                       + Math.Abs((dataPtrimgCopy + m.widthStep)[2] - (dataPtrimgCopy + nChan + m.widthStep)[1]));

                    p[8] = 0;
                    p[8] += (int)(Math.Abs((dataPtrimgCopy + nChan + m.widthStep)[0] - (dataPtrimgCopy + nChan)[0])
                        + Math.Abs((dataPtrimgCopy + nChan + m.widthStep)[1] - (dataPtrimgCopy + nChan)[1])
                        + Math.Abs((dataPtrimgCopy + nChan + m.widthStep)[2] - (dataPtrimgCopy + nChan)[2]));
                    p[8] += (int)(Math.Abs((dataPtrimgCopy + nChan + m.widthStep)[0] - (dataPtrimgCopy - nChan)[0])
                        + Math.Abs((dataPtrimgCopy + nChan + m.widthStep)[1] - (dataPtrimgCopy - nChan)[1])
                        + Math.Abs((dataPtrimgCopy + nChan + m.widthStep)[2] - (dataPtrimgCopy - nChan)[2])) * 2;
                    p[8] += (int)(Math.Abs((dataPtrimgCopy + nChan + m.widthStep)[0] - (dataPtrimgCopy + m.widthStep)[0])
                        + Math.Abs((dataPtrimgCopy + nChan + m.widthStep)[1] - (dataPtrimgCopy + m.widthStep)[1])
                        + Math.Abs((dataPtrimgCopy + nChan + m.widthStep)[2] - (dataPtrimgCopy + m.widthStep)[2]));
                    p[8] += (int)(Math.Abs((dataPtrimgCopy + nChan + m.widthStep)[0] - (dataPtrimgCopy - nChan + m.widthStep)[0])
                        + Math.Abs((dataPtrimgCopy + nChan + m.widthStep)[1] - (dataPtrimgCopy - nChan + m.widthStep)[1])
                        + Math.Abs((dataPtrimgCopy + nChan + m.widthStep)[2] - (dataPtrimgCopy - nChan + m.widthStep)[2]));

                    p[8] += (int)(Math.Abs((dataPtrimgCopy + nChan + m.widthStep)[0] - dataPtrimgCopy[0])
                        + Math.Abs((dataPtrimgCopy + nChan + m.widthStep)[1] - dataPtrimgCopy[1])
                        + Math.Abs((dataPtrimgCopy + nChan + m.widthStep)[2] - dataPtrimgCopy[2])) * 2;


                    less_val = Array.IndexOf(p, p.Min());

                    switch (less_val)
                    {
                        case 0:
                        case 3:
                            dataPtrimg[0] = dataPtrimgCopy[0];
                            dataPtrimg[1] = dataPtrimgCopy[1];
                            dataPtrimg[2] = dataPtrimgCopy[2];
                            break;
                        case 1:
                        case 4:
                            dataPtrimg[0] = (dataPtrimgCopy + nChan)[0];
                            dataPtrimg[1] = (dataPtrimgCopy + nChan)[1];
                            dataPtrimg[2] = (dataPtrimgCopy + nChan)[2];
                            break;
                        case 2:
                        case 5:
                            dataPtrimg[0] = (dataPtrimgCopy + m.widthStep)[0];
                            dataPtrimg[1] = (dataPtrimgCopy + m.widthStep)[1];
                            dataPtrimg[2] = (dataPtrimgCopy + m.widthStep)[2];
                            break;
                        case 6:
                            dataPtrimg[0] = (dataPtrimgCopy + m.widthStep + nChan)[0];
                            dataPtrimg[1] = (dataPtrimgCopy + m.widthStep + nChan)[1];
                            dataPtrimg[2] = (dataPtrimgCopy + m.widthStep + nChan)[2];
                            break;
                        case 7:
                            dataPtrimg[0] = (dataPtrimgCopy + m.widthStep + nChan)[0];
                            dataPtrimg[1] = (dataPtrimgCopy + m.widthStep + nChan)[1];
                            dataPtrimg[2] = (dataPtrimgCopy + m.widthStep + nChan)[2];
                            break;
                        case 8:
                            dataPtrimg[0] = (dataPtrimgCopy + m.widthStep + nChan)[0];
                            dataPtrimg[1] = (dataPtrimgCopy + m.widthStep + nChan)[1];
                            dataPtrimg[2] = (dataPtrimgCopy + m.widthStep + nChan)[2];
                            break;

                    }


                }


            }

        }

        //------------------------------------------(Histogram_Gray)------------------------------------------------------

        // OBG -> Finalizado

        public static int[] Histogram_Gray(Image<Bgr, byte> img)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;

                byte* dataPtrimg = (byte*)m.imageData.ToPointer();

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y;


                int[] histograma = new int[256];
                int valor_Pixel;



                for (y = 0; y < height; y++)
                {
                    for (x = 0; x < width; x++)
                    {
                        valor_Pixel = (int)Math.Round((float)(dataPtrimg[0] + dataPtrimg[1] + dataPtrimg[2]) / 3.0);
                        histograma[valor_Pixel]++;
                        dataPtrimg += nChan;

                    }

                    dataPtrimg += padding;
                }

                Histogram h = new Histogram(histograma);
                h.ShowDialog();

                return histograma;
            }

        }

        //------------------------------------------(Histogram_RGB)------------------------------------------------------

        // Facultativo -> Finalizado

        public static int[,] Histogram_RGB(Image<Bgr, byte> img)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;



                int width = img.Width;
                int height = img.Height;

                int x, y;
                int i;


                int[,] hist = new int[3, 256];
                for (i = 0; i < 3; i++)
                {
                    byte* dataPtrimg = (byte*)m.imageData.ToPointer();
                    int nChan = m.nChannels; // number of channels - 3
                    int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                    int valor_Pixel = 0;
                    switch (i)
                    {
                        case 0: //GREEN
                            for (y = 0; y < height; y++)
                            {
                                for (x = 0; x < width; x++)
                                {
                                    valor_Pixel = dataPtrimg[1];
                                    hist[1, valor_Pixel]++;
                                    dataPtrimg += nChan;
                                }
                                dataPtrimg += padding;
                            }
                            break;
                        case 1: // BLUE
                            for (y = 0; y < height; y++)
                            {
                                for (x = 0; x < width; x++)
                                {
                                    valor_Pixel = dataPtrimg[0];
                                    hist[0, valor_Pixel]++;
                                    dataPtrimg += nChan;
                                }
                                dataPtrimg += padding;
                            }
                            break;
                        case 2: // red
                            for (y = 0; y < height; y++)
                            {
                                for (x = 0; x < width; x++)
                                {
                                    valor_Pixel = dataPtrimg[2];
                                    hist[2, valor_Pixel]++;
                                    dataPtrimg += nChan;
                                }
                                dataPtrimg += padding;
                            }
                            break;
                    }
                }
                Histogram h = new Histogram(hist);
                h.ShowDialog();
                return hist;
            }
        }

        //------------------------------------------(Histogram_All)------------------------------------------------------

        // Facultativo -> Finalizado 

        public static int[,] Histogram_All(Image<Bgr, byte> img)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtrimg = (byte*)m.imageData.ToPointer();

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alignment bytes (padding)

                // Create a 4x256 histogram array
                int[,] histogram = new int[4, 256];

                int x, y;
                int grayValue;
                int redValue, greenValue, blueValue;

                for (y = 0; y < height; y++)
                {
                    for (x = 0; x < width; x++)
                    {
                        // Calculate gray value and update gray histogram
                        grayValue = (int)Math.Round((float)(dataPtrimg[0] + dataPtrimg[1] + dataPtrimg[2]) / 3.0);
                        histogram[0, grayValue]++;  // Gray histogram in the first row

                        // Update RGB histograms
                        blueValue = dataPtrimg[0];
                        greenValue = dataPtrimg[1];
                        redValue = dataPtrimg[2];

                        histogram[1, blueValue]++;  // Blue histogram in the second row
                        histogram[2, greenValue]++; // Green histogram in the third row
                        histogram[3, redValue]++;   // Red histogram in the fourth row

                        // Move to the next pixel
                        dataPtrimg += nChan;
                    }
                    // Move to the next row (accounting for padding)
                    dataPtrimg += padding;
                }

                // Show histogram dialog (if needed, this part is optional)
                Histogram histDialog = new Histogram(histogram);
                histDialog.ShowDialog();

                return histogram;
            }
        }

        //------------------------------------------(ConvertToBW)------------------------------------------------------

        // OBG -> Finalizado

        public static void ConvertToBW(Image<Bgr, byte> img, int threshold)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtrimg = (byte*)m.imageData.ToPointer();



                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int width = img.Width;
                int height = img.Height;
                int x, y;
                int pixel_color;
                for (y = 0; y < height; y++)
                {
                    for (x = 0; x < width; x++)
                    {
                        int valor_Pixel = (int)Math.Round((float)(dataPtrimg[0] + dataPtrimg[1] + dataPtrimg[2]) / 3.0);

                        if (valor_Pixel <= threshold)
                        {
                            pixel_color = 0;
                        }
                        else
                        {
                            pixel_color = 255;
                        }
                        dataPtrimg[0] = (byte)pixel_color;
                        dataPtrimg[1] = (byte)pixel_color;
                        dataPtrimg[2] = (byte)pixel_color;



                        dataPtrimg += nChan;
                    }
                    dataPtrimg += padding;
                }
            }
        }


        //------------------------------------------(ConvertToBW_OTSU)------------------------------------------------------

        // OBG -> Finalizado 


        public static void ConvertToBW_Otsu(Image<Bgr, byte> img)
        {
            unsafe
            {
                int[,] vetor_Hist = Histogram_All(img);

                double[] q1 = new double[256];
                double[] q2 = new double[256];
                double[] u1 = new double[256];
                double[] u2 = new double[256];

                double[] variancia = new double[256];

                double total = 0;
                int i;
                double prob;

                // Calcular o total de pixels na imagem
                for (i = 0; i <= 255; i++)
                {
                    total += vetor_Hist[0, i]; // Assumindo que a linha 0 é o histograma de tons de cinza
                }

                for (int t = 0; t <= 255; t++)
                {
                    q1[t] = 0;
                    q2[t] = 0;
                    u1[t] = 0;
                    u2[t] = 0;
                    variancia[t] = 0;

                    // Calcular q1 e u1 para o limite t
                    for (i = 0; i <= t; i++)
                    {
                        prob = vetor_Hist[0, i] / total; // Assumindo que a linha 0 é o histograma de tons de cinza
                        q1[t] += prob;
                        u1[t] += prob * i;
                    }

                    if (q1[t] != 0) u1[t] /= q1[t]; // Prevenir divisão por zero

                    // Calcular q2 e u2 para o limite t
                    for (i = t + 1; i <= 255; i++)
                    {
                        prob = vetor_Hist[0, i] / total; // Assumindo que a linha 0 é o histograma de tons de cinza
                        q2[t] += prob;
                        u2[t] += prob * i;
                    }

                    if (q2[t] != 0) u2[t] /= q2[t]; // Prevenir divisão por zero

                    // Calcular a variância entre classes
                    variancia[t] = q1[t] * q2[t] * Math.Pow(u1[t] - u2[t], 2);
                }

                // Encontrar o valor de threshold que maximiza a variância entre classes
                double treshold_Val = Array.IndexOf(variancia, variancia.Max());

                ConvertToBW(img, (int)treshold_Val);
            }
        }

        //===========================================================================================================//

        //                                      Deteção Jogo de Xadres                                               //

        //===========================================================================================================//


        //------------------------------------------(ColorToHSV)------------------------------------------------------

        //Terminado

        public static void ColorToHSV(Color color, out double hue, out double saturation, out double value)
        {
            int max = Math.Max(color.R, Math.Max(color.G, color.B));
            int min = Math.Min(color.R, Math.Min(color.G, color.B));

            hue = color.GetHue();
            saturation = (max == 0) ? 0 : 1d - (1d * min / max);
            value = max / 255d;

        }

        //------------------------------------------(HSV_BlueBin)------------------------------------------------------

        //Terminado

        public static void HSV_BlueBin(Image<Bgr, byte> img)
        {

            unsafe
            {
                MIplImage m = img.MIplImage;

                byte* dataPtr_write = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte blue, green, red;
                int width = img.Width;
                int height = img.Height;
                int widthstep = m.widthStep;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y;
                Color original;


                for (y = 0; y < height; y++)
                {
                    for (x = 0; x < width; x++)
                    {
                        original = Color.FromArgb((dataPtr_write + nChan * x + widthstep * y)[2], (dataPtr_write + nChan * x + widthstep * y)[1], (dataPtr_write + nChan * x + widthstep * y)[0]);
                        ColorToHSV(original, out double hue, out double saturation, out double value);


                        if ((hue >= 140 && hue <= 215) && saturation > 0.2 && value > 0.2)
                        {
                            blue = 255;
                            red = 255;
                            green = 255;


                        }
                        else
                        {
                            blue = 0;
                            red = 0;
                            green = 0;
                        }

                        (dataPtr_write + nChan * x + widthstep * y)[0] = (byte)blue;
                        (dataPtr_write + nChan * x + widthstep * y)[1] = (byte)green;
                        (dataPtr_write + nChan * x + widthstep * y)[2] = (byte)red;




                    }


                }


            }

        }

        //------------------------------------------(cortarMargens)------------------------------------------------------

        // Provavelmente mal confirmar !!!!!!!!

        public static Image<Bgr, byte> cortarMargens(Image<Bgr, byte> img, Image<Bgr, byte> imgor,double tr)
        {

            unsafe
            {
                MIplImage m = img.MIplImage;

                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                int width = img.Width;
                int height = img.Height;
                int widthstep = m.widthStep;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y;
                int[] max = new int[8];
                double[] hist_y = new double[height];
                double[] hist_x = new double[width];
                byte red, blue, green;

                System.Drawing.Rectangle recorta;

                for (y = 0; y < height; y++)
                {

                    
                    
                    //imagem se contorno
                    for (x = 0; x < width; x++)
                    {

                        red = (dataPtr + (nChan * (x)) + m.widthStep * (y))[0];
                        green = (dataPtr + (nChan * (x)) + m.widthStep * (y))[1];
                        blue = (dataPtr + (nChan * (x)) + m.widthStep * (y))[2];

                        if(blue == 255 && green == 255 && red == 255)
                        {
                            hist_y[y]++;

                        }

                    }


                    hist_y[y] = 100.0 * hist_y[y] / width;

                }

                int index_yt = 0;
                for (y = 0; y < height; y++)
                {
                    if (hist_y[y] > tr)
                    {
                        index_yt = y;
                        break;

                    }

                }

                int index_yb = height - 1;
                for (y = height - 1 ; y >= 0 ; y--)
                {
                    if (hist_y[y] > tr)
                    {
                        index_yb = y;
                        break;

                    }

                }





                
                for (x = 0; x < width; x++)
                {

      

                        //imagem se contorno
                    for (y = 0; y < height; y++)
                    {

                        red = (dataPtr + (nChan * (x)) + m.widthStep * (y))[0];
                        green = (dataPtr + (nChan * (x)) + m.widthStep * (y))[1];
                        blue = (dataPtr + (nChan * (x)) + m.widthStep * (y))[2];

                        if (blue == 255 && green == 255 && red == 255)
                        {
                            hist_x[x]++;

                        }

                    }


                    hist_x[x] = 100.0 * hist_x[x] / height;

                }

                int index_xt = 0;
                for (x = 0; x < width; x++)
                {
                    if (hist_x[x] > tr)
                    {
                        index_xt = x;
                        break;

                    }

                }

                int index_xb = width - 1;
                for (x = width - 1; x >= 0; x--)
                {
                    if (hist_x[x] > tr)
                    {
                        index_xb = x;
                        break;
                    }
                }
                
                recorta = new System.Drawing.Rectangle(index_xt, index_yt, index_xb - index_xt, index_yb - index_yt);
                Image<Bgr, byte> imagem = imgor.Copy(recorta);
                return imagem;



            }


        }

        //------------------------------------------(get_rectangle)------------------------------------------------------

        //Terminado

        public static Image<Bgr, byte> get_rectangle(Image<Bgr, byte> imagem, int index_x, int index_y)
        {
            unsafe
            {
                MIplImage m = imagem.MIplImage;

                byte* dataPtr_write = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte blue, green, red;
                int width = imagem.Width;
                int height = imagem.Height;
                int width_q = imagem.Width / 8;
                int height_q = imagem.Height / 8;

                int nChan = m.nChannels;

                System.Drawing.Rectangle recorta;

                int index_xt = (index_x - 1) * (imagem.Width / 8);
                int index_yt = (index_y - 1) * (imagem.Height / 8);

                recorta = new System.Drawing.Rectangle(index_xt, index_yt, imagem.Width / 8, imagem.Height / 8);
                Image<Bgr, byte> img = imagem.Copy(recorta);
            
                return img;
            }
        }

        //------------------------------------------(binarizacao Peca)------------------------------------------------------
        public static void binarizacaoPeca(Image<Bgr, byte> img)
        {

            unsafe
            {
                MIplImage m = img.MIplImage;

                byte* dataPtr_write = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte blue, green, red;
                int width = img.Width;
                int height = img.Height;
                int widthstep = m.widthStep;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y;
                Color original;


                for (y = 0; y < height; y++)
                {
                    for (x = 0; x < width; x++)
                    {
                        original = Color.FromArgb((dataPtr_write + nChan * x + widthstep * y)[2], (dataPtr_write + nChan * x + widthstep * y)[1], (dataPtr_write + nChan * x + widthstep * y)[0]);
                        ColorToHSV(original, out double hue, out double saturation, out double value);


                        if ( value <= 0.3)
                        {
                            blue = 255;
                            red = 255;
                            green = 255;


                        }
                        else
                        {
                            blue = 0;
                            red = 0;
                            green = 0;
                        }

                        (dataPtr_write + nChan * x + widthstep * y)[0] = (byte)blue;
                        (dataPtr_write + nChan * x + widthstep * y)[1] = (byte)green;
                        (dataPtr_write + nChan * x + widthstep * y)[2] = (byte)red;




                    }


                }


            }

        }


        public static int compara_pixels(Image<Bgr, byte> img, Image<Bgr, byte> imgBD)
        {

            unsafe
            {

                MIplImage m = img.MIplImage;
                byte* dataPtr_img = (byte*)m.imageData.ToPointer(); // Pointer to the image
                MIplImage mBD = imgBD.MIplImage;
                byte* dataPtr_imgBD = (byte*)mBD.imageData.ToPointer();
                byte blue, green, red;
                int width = img.Width;
                int height = img.Height;
                int widthstep = m.widthStep;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y;
                int numPixelsDiferentes = 0;

                
                


                for (y = 0; y < height; y++)
                {
                    for (x = 0; x < width; x++)
                    {
                        numPixelsDiferentes = numPixelsDiferentes + Math.Abs((dataPtr_img)[0] - (dataPtr_imgBD)[0]);
                        dataPtr_img += nChan;
                        dataPtr_imgBD += nChan;
                    }

                    dataPtr_img += padding;
                    dataPtr_imgBD += padding;

                }

                return numPixelsDiferentes;
            }
        }







        public static string compara_imagens(Image<Bgr, byte> img)
        {
            unsafe
            {



                string[] Base_Dados = Directory.GetFiles(@"C:\Users\Tomás Nave\Documents\Faculdade\2ºAno\2ºSemestre\Processamento de Imagem 2º_2Sem\BD Chess-20240503\BD Chess");
                int aux = Base_Dados.Length;
                Image<Bgr, byte> img_BD;
                double[] relacoes = new double[aux];
                int width1 = 12;
                int height1 = 24;
                Image<Bgr, Byte> imgUndo = img.Copy();
                binarizacaoPeca(img);
                Image<Bgr, Byte> img1 = ImageClass.cortarMargens(img,imgUndo,1);

                img1 = img1.Resize(width1, height1, INTER.CV_INTER_CUBIC);
                binarizacaoPeca(img1);
                //PERCORRER BASE DE DADOS
                for (int B_D = 0; B_D < aux; B_D++)
                {

                    img_BD = new Image<Bgr, Byte>(Base_Dados[B_D]);

                    Image<Bgr, Byte> imgBDUndo = img_BD.Copy();

                    binarizacaoPeca(img_BD);

                    Image<Bgr, Byte> img2 = ImageClass.cortarMargens(img_BD,imgBDUndo, 1);

                    img2 = img2.Resize(width1, height1, INTER.CV_INTER_CUBIC);

                    binarizacaoPeca(img2);

                    relacoes[B_D] = compara_pixels(img1, img2); //percentagens de igualdade



                }
                double Min = relacoes.Min();
                int index = Array.IndexOf(relacoes, Min);
                string path = Base_Dados[index];
                string result = Path.GetFileNameWithoutExtension(path); //o Nome da peca correspondente
                Console.WriteLine(result);
                return result;
            }


        }

        public static int isEmpty(Image<Bgr, byte> img)
        {

            unsafe
            {
                MIplImage m = img.MIplImage;


                byte* dataPtrImg = (byte*)m.imageData.ToPointer();
                int nChan = m.nChannels;
                int padding = m.widthStep - m.nChannels * m.width;
                int x, y;
                int countPB = 0;


                for (y = 0; y < m.height; y++)
                {
                    for (x = 0; x < m.width; x++)
                    {
                        if ((dataPtrImg + nChan * x + m.widthStep * y)[0] == 255)
                        {
                            countPB++;

                        }
                    }
                }
                if (100*countPB / (m.width * m.height) < 2)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }

            }
        }

    }

}