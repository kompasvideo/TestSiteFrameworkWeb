using Veza.HeatExchanger.Exceptions;
using System;
using System.Collections.Generic;
using Veza.HeatExchanger.Interfaces.Refrigerants;

namespace Veza.HeatExchanger.Services.Refrigerants
{
    /// <summary>
    /// Класс для перевода температуры в давление для хладогента R134a
    /// Данные взяты из программы Select 8
    /// </summary>
    sealed internal class RefrigerantR134a : IRefrigerant
    {
        int max = 102;
        int min = -41;
        Dictionary<int, double> tempToPres = new Dictionary<int, double>()
        {
            {-40, 0.51},
            {-39, 0.54},
            {-38, 0.57},
            {-37, 0.60},
            {-36, 0.63},
            {-35, 0.66},
            {-34, 0.69},
            {-33, 0.73},
            {-32, 0.77},
            {-31, 0.8},
            {-30, 0.84},
            {-29, 0.88},
            {-28, 0.93},
            {-27, 0.97},
            {-26, 1.02},
            {-25, 1.06},
            {-24, 1.11},
            {-23, 1.16},
            {-22, 1.22},
            {-21, 1.27},
            {-20, 1.33},
            {-19, 1.39},
            {-18, 1.45},
            {-17, 1.51},
            {-16, 1.57},
            {-15, 1.64},
            {-14, 1.71},
            {-13, 1.78},
            {-12, 1.85},
            {-11, 1.93},
            {-10, 2.01},
            {-9, 2.09},
            {-8, 2.17},
            {-7, 2.26},
            {-6, 2.34},
            {-5, 2.43},
            {-4, 2.53},
            {-3, 2.62},
            {-2, 2.72},
            {-1, 2.82},
            { 0, 2.93},
            { 1, 3.04},
            { 2, 3.15},
            { 3, 3.26},
            { 4, 3.38},
            { 5, 3.5},
            { 6, 3.62},
            { 7, 3.75},
            { 8, 3.88},
            { 9, 4.01},
            { 10, 4.15},
            { 11, 4.29},
            { 12, 4.43},
            { 13, 4.58},
            { 14, 4.73},
            { 15, 4.88},
            { 16, 5.04},
            { 17, 5.21},
            { 18, 5.37},
            { 19, 5.54},
            { 20, 5.72},
            { 21, 5.9},
            { 22, 6.08},
            { 23, 6.27},
            { 24, 6.46},
            { 25, 6.65},
            { 26, 6.85},
            { 27, 7.06},
            { 28, 7.27},
            { 29, 7.48},
            { 30, 7.7},
            { 31, 7.93},
            { 32, 8.15},
            { 33, 8.39},
            { 34, 8.63},
            { 35, 8.87},
            { 36, 9.12},
            { 37, 9.37},
            { 38, 9.63},
            { 39, 9.89},
            {40, 10.16},
            {41, 10.44},
            {42, 10.72},
            {43, 11.01},
            {44, 11.3},
            {45, 11.5},
            {46, 11.9},
            {47, 12.21},
            {48, 12.52},
            {49, 12.85},
            {50, 13.17},
            {51, 13.51},
            {52, 13.85},
            {53, 14.2},
            {54, 14.55},
            {55, 14.91},
            {56, 15.28},
            {57, 15.65},
            {58, 16.03},
            {59, 16.42},
            {60, 16.81},
            {61, 17.21},
            {62, 17.62},
            {63, 18.04},
            {64, 18.46},
            {65, 18.89},
            {66, 19.33},
            {67, 19.78},
            {68, 20.23},
            {69, 20.70},
            {70, 21.17},
            {71, 21.65},
            {72, 22.13},
            {73, 22.63},
            {74, 23.13},
            {75, 23.65},
            {76, 24.17},
            {77, 24.7},
            {78, 25.24},
            {79, 25.79},
            {80, 26.34},
            {81, 26.91},
            {82, 27.49},
            {83, 28.08},
            {84, 28.67},
            {85, 29.28},
            {86, 29.9},
            {87, 30.53},
            {88, 31.17},
            {89, 31.81},
            {90, 32.47},
            {91, 33.15},
            {92, 33.83},
            {93, 34.52},
            {94, 35.23},
            {95, 35.94},
            {96, 36.67},
            {97, 37.41},
            {98, 38.17},
            {99, 38.94},
            {100, 39.71},
            {101, 40.43},
        };
        Dictionary<double, int> presToTemp = new Dictionary<double, int>()
        {
            {0.51, -40},
            {0.54, -39},
            {0.57, -38},
            {0.60, -37},
            {0.63, -36},
            {0.66, -35},
            {0.69, -34},
            {0.73, -33},
            {0.77, -32},
            {0.8, -31},
            {0.84, -30},
            {0.88, -29},
            {0.93, -28},
            {0.97, -27},
            {1.02, -26},
            {1.06, -25},
            {1.11, -24},
            {1.16, -23},
            {1.22, -22},
            {1.27, -21},
            {1.33, -20},
            {1.39, -19},
            {1.45, -18},
            {1.51, -17},
            {1.57, -16},
            {1.64, -15},
            {1.71, -14},
            {1.78, -13},
            {1.85, -12},
            {1.93, -11},
            {2.01, -10},
            {2.09, -9},
            {2.17, -8},
            {2.26, -7},
            {2.34, -6},
            {2.43, -5},
            {2.53, -4},
            {2.62, -3},
            {2.72, -2},
            {2.82, -1},
            {2.93, 0},
            {3.04, 1},
            {3.15, 2},
            {3.26, 3},
            {3.38, 4},
            {3.5, 5},
            {3.62, 6},
            {3.75, 7},
            {3.88, 8},
            {4.01, 9},
            {4.15, 10},
            {4.29, 11},
            {4.43, 12},
            {4.58, 13},
            {4.73, 14},
            {4.88, 15},
            {5.04, 16},
            {5.21, 17},
            {5.37, 18},
            {5.54, 19},
            {5.72, 20},
            {5.9,  21},
            {6.08,  22},
            {6.27,  23},
            {6.46,  24},
            {6.65,  25},
            {6.85,  26},
            {7.06,  27},
            {7.27,  28},
            {7.48,  29},
            {7.7,  30},
            {7.93,  31},
            {8.15,  32},
            {8.39,  33},
            {8.63,  34},
            {8.87,  35},
            {9.12,  36},
            {9.37,  37},
            {9.63,  38},
            {9.89,  39},
            {10.16, 40},
            {10.44, 41},
            {10.72, 42},
            {11.01, 43},
            {11.3, 44},
            {11.5, 45},
            {11.9, 46},
            {12.21, 47},
            {12.52, 48},
            {12.85, 49},
            {13.17, 50},
            {13.51, 51},
            {13.85, 52},
            {14.2, 53},
            {14.55, 54},
            {14.91, 55},
            {15.28, 56},
            {15.65, 57},
            {16.03, 58},
            {16.42, 59},
            {16.81, 60},
            {17.21, 61},
            {17.62, 62},
            {18.04, 63},
            {18.46, 64},
            {18.89, 65},
            {19.33, 66},
            {19.78, 67},
            {20.23, 68},
            {20.70, 69},
            {21.17, 70},
            {21.65, 71},
            {22.13, 72},
            {22.63, 73},
            {23.13, 74},
            {23.65, 75},
            {24.17, 76},
            {24.7, 77},
            {25.24, 78},
            {25.79, 79},
            {26.34, 80},
            {26.91, 81},
            {27.49, 82},
            {28.08, 83},
            {28.67, 84},
            {29.28, 85},
            {29.9, 86},
            {30.53, 87},
            {31.17, 88},
            {31.81, 89},
            {32.47, 90},
            {33.15, 91},
            {33.83, 92},
            {34.52, 93},
            {35.23, 94},
            {35.94, 95},
            {36.67, 96},
            {37.41, 97},
            {38.17, 98},
            {38.94, 99},
            {39.71, 100},
            {40.43, 101},
        };

        #region Температура кипения
        public double ToPressure(double temperature)
        {
            int temp = (int)temperature;
            if (temp < max)
            {
                if (temp > min)
                {
                    double result = tempToPres[temp];
                    if ((temperature - temp) == 0)
                        return result;
                    if (temperature > 0)
                    {
                        double resultAbove = tempToPres[temp + 1];
                        return result + (resultAbove - result) * (temperature - temp);
                    }
                    if (temperature < 0)
                    {
                        double resultLess = tempToPres[temp - 1];
                        return result + (result - resultLess) * (temperature - temp);
                    }
                }
            }
            throw new TempToPresException(Calculation.TO.Main.Properties.Resources.ErrorTempEvap); 
        }

        public double ToTemperature(double pressure)
        {
            int value;
            if (presToTemp.TryGetValue(pressure, out value))
            {
                return value;
            }
            else
            {
                //var keys = presToTemp.Keys;
                double delta = 1000;
                double keyRes = 0;
                foreach (var key in presToTemp.Keys)
                {
                    double res = Math.Abs(pressure - key);
                    if (res < delta)
                    {
                        delta = res;
                        keyRes = key;
                    }
                }
                if (keyRes != 0)
                {
                    if (presToTemp.TryGetValue(keyRes, out value))
                    {
                        if (pressure > keyRes)
                        {
                            double keyAbove = tempToPres[value + 1];
                            return (pressure - keyRes) / (keyAbove - keyRes) + value;
                        }
                        else
                        {
                            if (value > (min + 1))
                            {
                                double keyLess = tempToPres[value - 1];
                                return value - (keyRes - pressure) / (keyRes - keyLess);
                            }
                        }
                    }
                }
            }
            throw new TempToPresException(Calculation.TO.Main.Properties.Resources.ErrorPresEvap);
        }
        #endregion

        #region Температура конденсации

        public double ToCondPressure(double temperature)
        {
            int temp = (int)temperature;
            if (temp < max)
            {
                if (temp > min)
                {
                    double result = tempToPres[temp];
                    if ((temperature - temp) == 0)
                        return result;

                    double resultAbove = tempToPres[temp + 1];
                    return result + (resultAbove - result) * (temperature - temp);
                }
            }
            throw new TempToPresException(Calculation.TO.Main.Properties.Resources.ErrorTempCond);
        }

        public double ToCondTemperature(double pressure)
        {
            int value;
            if (presToTemp.TryGetValue(pressure, out value))
            {
                return value;
            }
            else
            {
                //var keys = presToTemp.Keys;
                double delta = 1000;
                double keyRes = 0;
                foreach (var key in presToTemp.Keys)
                {
                    double res = Math.Abs(pressure - key);
                    if (res < delta)
                    {
                        delta = res;
                        keyRes = key;
                    }
                }
                if (keyRes != 0)
                {
                    if (presToTemp.TryGetValue(keyRes, out value))
                    {
                        if (pressure > keyRes)
                        {
                            double keyAbove = tempToPres[value + 1];
                            return (pressure - keyRes) / (keyAbove - keyRes) + value;
                        }
                        else
                        {
                            if (value > (min + 1))
                            {
                                if (value > (min + 1))
                                {
                                    double keyLess = tempToPres[value - 1];
                                    return value - (keyRes - pressure) / (keyRes - keyLess);
                                }
                            }
                        }
                    }
                }
            }
            throw new TempToPresException(Calculation.TO.Main.Properties.Resources.ErrorPresCond);
        }
        #endregion

        #region Переохлаждение
        public double ToSubCol(double tempCond, double temperature)
        {
            int temp = (int)tempCond;
            if (temp < max)
            {
                if (temp > min)
                {
                    double result = temp;
                    if ((tempCond - temp) == 0)
                    {
                        return result - temperature;
                    }
                    double resultAbove = temp + 1;
                    return result + (resultAbove - result) * (tempCond - temp) - temperature;
                }
            }
            throw new TempToPresException(Calculation.TO.Main.Properties.Resources.ErrorSubCol);
        }

        public double ToSubColTemperature(double tempCond, double tempSubCol)
        {
            int temp = (int)tempCond;
            if (temp < max)
            {
                if (temp > min)
                {
                    double result = temp;
                    if ((tempCond - temp) == 0)
                    {
                        return result - tempSubCol;
                    }
                    double resultAbove = temp + 1;
                    return result + (resultAbove - result) * (tempCond - temp) - tempSubCol;
                }
            }
            throw new TempToPresException(Calculation.TO.Main.Properties.Resources.ErrorSubColTemp);
        }
        #endregion
    }
}
