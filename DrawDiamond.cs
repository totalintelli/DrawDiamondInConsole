using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
파일 이름: DrawDiamond.cs
기    능: 다이아몬드 모양으로 별을 출력한다.
작 성 자: 송 용 단
작성 일자: 2016년 2월 26일 
*/

namespace DrawDiamondInConsole
{
    class DrawDiamond
    {
        public enum ImageType
        {
            Diamond,
            EmptyDiamond,
            Pyramid,
            ReversePyramid
        }
        static void Main(string[] args)
        {
            int number;
            char[,] image;
            int row;
            int column;
            DrawDiamond drawDiamond = new DrawDiamond();
            ImageType imgType = ImageType.Diamond;
            int imgTypeNumber = 0;

            Console.WriteLine("---------------------------");
            Console.WriteLine("1. 다이아몬드");
            Console.WriteLine("2. 빈 다이아몬드");
            Console.WriteLine("3. 피라미드");
            Console.WriteLine("4. 뒤집힌 피라미드");
            Console.WriteLine("보고 싶은 문양을 고르세요.");
            Console.WriteLine("---------------------------");
            imgTypeNumber = Int32.Parse(Console.ReadLine());

            switch(imgTypeNumber)
            {
                case 1:
                    imgType = ImageType.Diamond;
                    break;
                case 2:
                    imgType = ImageType.EmptyDiamond;
                    break;
                case 3:
                    imgType = ImageType.Pyramid;
                    break;
                case 4:
                    imgType = ImageType.ReversePyramid;
                    break;
                default:
                    break;
            }

            // 수를 사용자로부터 입력받는다.
            number = drawDiamond.Input(imgType);

            if (number > 0)
            {
                // 다이아몬드와 피라미드 이미지를 만든다
                drawDiamond.MakeDiamondAndPyramidImage(number, imgType, out image, out row, out column);

                // 이미지를 출력한다.
                drawDiamond.Output(image, row, column);
            }

            else
            {
                Console.Write("0보다 큰 자연수를 입력하세요.");
                Console.ReadKey();
            }
        }




        /*
        함수 이름: Input
        기    능: 수를 입력받는다.
        입    력: 이미지 타입 번호
        출    력: 수
        */
        int Input(ImageType imgType)
        {
            int number = 0; // 수

            if(imgType == ImageType.Diamond || imgType == ImageType.EmptyDiamond)
            {
                // 프로그램 설명을 출력한다.
                Console.Write("Program for displaying pattern of *.\nEnter the maximum number of *: ");
                number = Int32.Parse(Console.ReadLine());
                Console.Write("\nHere is the Diamond of Stars\n");
            }
            else if(imgType == ImageType.Pyramid || imgType == ImageType.ReversePyramid)
            {
                // 프로그램 설명을 출력한다.
                Console.Write("마지막에 출력할 별의 수를 입력하세요! 피라미드를 만듭니다.");
                number = Int32.Parse(Console.ReadLine());
            }


            return number;
        }



        /*
        함수 이름: Output
        기    능: 이미지를 콘솔에 출력한다.
        입    력: 이미지
        출    력: 없음
        */
        void Output(char[,] image, int row, int column)
        {
            // 배열을 출력한다.
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    Console.Write(image[i, j]);
                }
                // 다음 줄로 이동한다.
                Console.Write("\n");
            }
            Console.ReadKey();
        }
        
        /*
        함수 이름: MakeDiamontAndPyramidImage
        기    능: 다이아몬드와 피라미드 이미지를 만든다.
        입    력: 수
        출    력: 이미지, 줄의 개수, 열의 개수

        */
       
        void MakeDiamondAndPyramidImage(int number, ImageType imgType, out char[,] image, out int row, out int column)
        {
            int start = 0; // 별표가 들어가는 시작 위치(Ordinal number), Ordinal number는 프로그래밍에서 0부터 시작한다. 
            int end;       // 별표가 들어가는 마지막 위치(Ordinal number)
            int i;         // 줄의 위치(Ordinal number) vs row -> 줄의 개수 -> Cardinal number -> 1부터 시작한다.
            int j;         // 열의 위치(Ordinal number) vs column -> 열의 개수 -> Cardinal number

            image = null;
            row = 0;
            column = 0;

            if(imgType == ImageType.Diamond || imgType == ImageType.EmptyDiamond)
            {
                // 이미지를 만들 준비를 한다.
                row = number * 2 - 1;
                column = row;
                end = column - 1;
                image = new char[row, column];

                // 이미지를 만든다.
                i = number - 1;
                while (i >= 0)
                {
                    j = 0;
                    while (j < column)
                    {
                        if(imgType == ImageType.EmptyDiamond)
                        {
                            if (j == start || j == end)
                            {
                                image[i, j] = '*';
                                image[column - i - 1, j] = '*';
                            }
                            else
                            {
                                image[i, j] = ' ';
                                image[column - i - 1, j] = ' ';
                            }

                        }
                        else
                        {
                            if (j >= start && j <= end)
                            {
                                image[i, j] = '*';
                                image[column - i - 1, j] = '*';
                            }
                            else
                            {
                                image[i, j] = ' ';
                                image[column - i - 1, j] = ' ';
                            }
                        }
                        j++;

                    }
                    start++;
                    end--;
                    i--;
                }
            }


            if(imgType == ImageType.Pyramid || imgType == ImageType.ReversePyramid)
            {
                // 수가 짝수이면 별의 개수에 맞춘다.
                if (number % 2 == 0)
                {
                    number++;
                }

                // 줄의 개수를 구한다.
                row = (number + 1) / 2;

                // 열의 개수를 구한다.
                column = number;

                // 마지막 위치를 구한다.
                end = number - 1;

                // 이미지를 구한다.
                image = new char[row, column];


                if (imgType == ImageType.Pyramid)
                {
                    i = row - 1;

                    while (i >= 0)
                    {
                        j = 0;
                        while (j < column)
                        {

                            if (j >= start && j <= end)
                            {
                                image[i, j] = '*';
                            }
                            else
                            {
                                image[i, j] = ' ';
                            }
                            j++;
                        }
                        start++;    
                        end--;
                        i--;
                    }
                }
                else
                {
                    i = 0;
                    while (i < row)
                    {
                        j = 0;
                        while (j < column)
                        {
                            if (j >= start && j <= end)
                            {
                                image[i, j] = '*';
                            }
                            else
                            {
                                image[i, j] = ' ';
                            }
                            j++;
                        }
                        start++;
                        end--;
                        i++;
                    }
                }
                    
            }
        }
    }
}
