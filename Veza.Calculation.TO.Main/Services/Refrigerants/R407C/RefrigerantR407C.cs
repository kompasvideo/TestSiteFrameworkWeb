using Veza.HeatExchanger.Exceptions;
using System;
using System.Collections.Generic;
using Veza.HeatExchanger.Interfaces.Refrigerants;

namespace Veza.HeatExchanger.Services.Refrigerants
{
    /// <summary>
    /// Класс для перевода температуры в давление для хладогента R407C
    /// Данные взяты из программы Select 8
    /// </summary>
    sealed internal class RefrigerantR407C : IRefrigerant
    {
        int max = 87;
        int min = -51;       

        Dictionary<int, double> tempToPres = new Dictionary<int, double>()
        {
            {-50, 0.51},
            {-49, 0.53},
            {-48, 0.56},
            {-47, 0.6},
            {-46, 0.63},
            {-45, 0.66},
            {-44, 0.7},
            {-43, 0.74},
            {-42, 0.78},
            {-41, 0.82},
            {-40, 0.86},
            {-39, 0.91},
            {-38, 0.95},
            {-37, 1},
            {-36, 1.05},
            {-35, 1.10},
            {-34, 1.16},
            {-33, 1.21},
            {-32, 1.27},
            {-31, 1.33},
            {-30, 1.39},
            {-29, 1.46},
            {-28, 1.52},
            {-27, 1.59},
            {-26, 1.67},
            {-25, 1.74},
            {-24, 1.82},
            {-23, 1.9},
            {-22, 1.98},
            {-21, 2.06},
            {-20, 2.15},
            {-19, 2.24},
            {-18, 2.34},
            {-17, 2.43},
            {-16, 2.53},
            {-15, 2.64},
            {-14, 2.74},
            {-13, 2.85},
            {-12, 2.97},
            {-11, 3.08},
            {-10, 3.2},
            {-9, 3.33},
            {-8, 3.45},
            {-7, 3.59},
            {-6, 3.72},
            {-5, 3.86},
            {-4, 4.0},
            {-3, 4.15},
            {-2, 4.3},
            {-1, 4.45},
            { 0, 4.61},
            { 1, 4.77},
            { 2, 4.94},
            { 3, 5.11},
            { 4, 5.29},
            { 5, 5.47},
            { 6, 5.66},
            { 7, 5.85},
            { 8, 6.04},
            { 9, 6.24},
            { 10, 6.45},
            { 11, 6.66},
            { 12, 6.87},
            { 13, 7.1},
            { 14, 7.32},
            { 15, 7.55},
            { 16, 7.79},
            { 17, 8.03},
            { 18, 8.28},
            { 19, 8.54},
            { 20, 8.8},
            {21, 9.06},
            {22, 9.33},
            {23, 9.61},
            {24, 9.9},
            {25, 10.19},
            {26, 10.49},
            {27, 10.79},
            {28, 11.1},
            {29, 14.42},
            {30, 11.74},
            {31, 12.07},
            {32, 12.41},
            {33, 12.76},
            {34, 13.11},
            {35, 13.47},
            {36, 13.84},
            {37, 14.22},
            {38, 14.6},
            {39, 14.99},
            {40, 15.39},
            {41, 15.8},
            {42, 16.21},
            {43, 16.64},
            {44, 17.07},
            {45, 17.51},
            {46, 17.96},
            {47, 18.42},
            {48, 18.89},
            {49, 19.36},
            {50, 19.85},
            {51, 20.34},
            {52, 20.85},
            {53, 21.36},
            {54, 21.89},
            {55, 22.42},
            {56, 22.97},
            {57, 23.52},
            {58, 24.09},
            {59, 24.67},
            {60, 25.26},
            {61, 25.85},
            {62, 26.46},
            {63, 27.09},
            {64, 27.72},
            {65, 28.36},
            {66, 29.02},
            {67, 29.69},
            {68, 30.37},
            {69, 31.06},
            {70, 31.77},
            {71, 32.49},
            {72, 33.22},
            {73, 33.97},
            {74, 34.73},
            {75, 35.51},
            {76, 36.3},
            {77, 37.1},
            {78, 37.92},
            {79, 38.75},
            {80, 39.6},
            {81, 40.46},
            {82, 41.34},
            {83, 42.23},
            {84, 43.15},
            {85, 44.08},
            {86, 44.94},
        };
        Dictionary<double, int> presToTemp = new Dictionary<double, int>()
        {
            {0.51, -50},
            {0.53, -49},
            {0.56, -48},
            {0.6 , -47},
            {0.63, -46},
            {0.66, -45},
            {0.7 , -44},
            {0.74, -43},
            {0.78, -42},
            {0.82, -41},
            {0.86, -40},
            {0.91, -39},
            {0.95, -38},
            {1   , -37},
            {1.05, -36},
            {1.10, -35},
            {1.16, -34},
            {1.21, -33},
            {1.27, -32},
            {1.33, -31},
            {1.39, -30},
            {1.46, -29},
            {1.52, -28},
            {1.59, -27},
            {1.67, -26},
            {1.74, -25},
            {1.82, -24},
            {1.9 , -23},
            {1.98, -22},
            {2.06, -21},
            {2.15, -20},
            {2.24, -19},
            {2.34, -18},
            {2.43, -17},
            {2.53, -16},
            {2.64, -15},
            {2.74, -14},
            {2.85, -13},
            {2.97, -12},
            {3.08, -11},
            {3.02, -10},
            {3.33, -9},
            {3.45, -8},
            {3.59, -7},
            {3.72, -6},
            {3.86, -5},
            {4.0, -4},
            {4.15, -3},
            {4.3, -2},
            {4.45, -1},
            {4.61, 0},
            { 4.77, 1},
            { 4.94, 2},
            { 5.11, 3},
            { 5.29, 4},
            { 5.47, 5},
            { 5.66, 6},
            { 5.85, 7},
            { 6.04, 8},
            { 6.24, 9},
            { 6.45, 10},
            { 6.66, 11},
            { 6.87, 12},
            { 7.1, 13},
            { 7.32, 14},
            { 7.55, 15},
            { 7.79, 16},
            { 8.03, 17},
            { 8.28, 18},
            { 8.54, 19},
            { 8.8, 20},
            {9.06, 21},
            {9.33, 22},
            {9.61, 23},
            {9.9, 24},
            {10.19, 25},
            {10.49, 26},
            {10.79, 27},
            {11.1, 28},
            {14.42, 29},
            {11.74, 30},
            {12.07, 31},
            {12.41, 32},
            {12.76, 33},
            {13.11, 34},
            {13.47, 35},
            {13.84, 36},
            {14.22, 37},
            {14.6, 38},
            {14.99, 39},
            {15.39, 40},
            {15.8, 41},
            {16.21, 42},
            {16.64, 43},
            {17.07, 44},
            {17.51, 45},
            {17.96, 46},
            {18.42, 47},
            {18.89, 48},
            {19.36, 49},
            {19.85, 50},
            {20.34, 51},
            {20.85, 52},
            {21.36, 53},
            {21.89, 54},
            {22.42, 55},
            {22.97, 56},
            {23.52, 57},
            {24.09, 58},
            {24.67, 59},
            {25.26, 60},
            {25.85, 61},
            {26.46, 62},
            {27.09, 63},
            {27.72, 64},
            {28.36, 65},
            {29.02, 66},
            {29.69, 67},
            {30.37, 68},
            {31.06, 69},
            {31.77, 70},
            {32.49, 71},
            {33.22, 72},
            {33.97, 73},
            {34.73, 74},
            {35.51, 75},
            {36.3, 76},
            {37.1, 77},
            {37.92, 78},
            {38.75, 79},
            {39.6 , 80},
            {40.46, 81},
            {41.34, 82},
            {42.23, 83},
            {43.15, 84},
            {44.08, 85},
            {44.94, 86},
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
        int maxSC = 87;
        int minSC = -51;
        Dictionary<int, double> scToTemp = new Dictionary<int, double>()
        {
            {-50, -57.36},
            {-49, -56.34},
            {-48, -55.32},
            {-47, -54.3},
            {-46, -53.28},
            {-45, -52.26},
            {-44, -51.24},
            {-43, -50.22},
            {-42, -49.2},
            {-41, -48.18},
            {-40, -47.16},
            {-39, -46.14},
            {-38, -45.11},
            {-37, -44.09},
            {-36, -43.07},
            {-35, -42.05},
            {-34, -41.03},
            {-33, -40.01},
            {-32, -38.99},
            {-31, -37.97},
            {-30, -36.95},
            {-29, -35.92},
            {-28, -34.9},
            {-27, -33.88},
            {-26, -32.86},
            {-25, -31.84},
            {-24, -30.82},
            {-23, -29.79},
            {-22, -28.77},
            {-21, -27.75},
            {-20, -26.73},
            {-19, -25.71},
            {-18, -24.68},
            {-17, -23.66},
            {-16, -22.64},
            {-15, -21.62},
            {-14, -20.6},
            {-13, -19.57},
            {-12, -18.55},
            {-11, -17.53},
            {-10, -16.5},
            {-9, -15.48},
            {-8, -14.46},
            {-7, -13.43},
            {-6, -12.41},
            {-5, -11.39},
            {-4, -10.36},
            {-3, -9.34},
            {-2, -8.32},
            {-1, -7.29},
            {0, -6.27},
            {1, -5.24},
            {2, -4.22},
            {3, -3.19},
            {4, -2.17},
            {5, -1.14},
            {6, -0.12},
            {7, 0.91},
            {8, 1.93},
            {9, 2.96},
            {10, 3.99},
            {11, 5.01},
            {12, 6.04},
            {13, 7.07},
            {14, 8.09},
            {15, 9.12},
            {16, 10.15},
            {17, 11.18},
            {18, 12.21},
            {19, 13.24},
            {20, 14.26},
            {21, 15.29},
            {22, 16.32},
            {23, 17.35},
            {24, 18.38},
            {25, 19.42},
            {26, 20.45},
            {27, 21.48},
            {28, 22.51},
            {29, 23.54},
            {30, 24.58},
            {31, 25.61},
            {32, 26.65},
            {33, 27.68},
            {34, 28.72},
            {35, 29.75},
            {36, 30.79},
            {37, 31.82},
            {38, 32.86},
            {39, 33.9},
            {40, 34.94},
            {41, 35.98},
            {42, 37.02},
            {43, 38.06},
            {44, 39.1},
            {45, 40.15},
            {46, 41.19},
            {47, 42.23},
            {48, 43.28},
            {49, 44.32},
            {50, 45.37},
            {51, 46.42},
            {52, 47.47},
            {53, 48.52},
            {54, 49.57},
            {55, 50.62},
            {56, 51.67},
            {57, 52.73},
            {58, 53.78},
            {59, 54.84},
            {60, 55.9},
            {61, 56.96},
            {62, 58.02},
            {63, 59.08},
            {64, 60.14},
            {65, 61.21},
            {66, 62.28},
            {67, 63.34},
            {68, 64.41},
            {69, 65.49},
            {70, 66.56},
            {71, 67.63},
            {72, 68.71},
            {73, 69.79},
            {74, 70.87},
            {75, 71.95},
            {76, 73.03},
            {77, 74.12},
            {78, 75.21},
            {79, 76.3},
            {80, 77.39},
            {81, 78.49},
            {82, 79.58},
            {83, 80.68},
            {84, 81.78},
            {85, 82.89},
            {86, 82.98},
        };
        public double ToSubCol(double tempCond, double temperature)
        {
            if (tempCond == 0 && temperature == 0) return 0;
            int temp = (int)tempCond;
            if (temp < maxSC)
            {
                if (temp > minSC)
                {
                    double result = scToTemp[temp];
                    if ((tempCond - temp) == 0)
                    {
                        return result - temperature;
                    }
                    double resultAbove = scToTemp[temp + 1];
                    return result + (resultAbove - result) * (tempCond - temp) - temperature;
                }
            }
            throw new TempToPresException(Calculation.TO.Main.Properties.Resources.ErrorSubCol);
        }

        public double ToSubColTemperature(double tempCond, double tempSubCol)
        {
            


            int temp = (int)tempCond;
            if (temp < maxSC)
            {
                if (temp > minSC)
                {
                    double result = scToTemp[temp];
                    if ((tempCond - temp) == 0)
                    {
                        return result - tempSubCol;
                    }
                    double resultAbove = scToTemp[temp + 1];
                    return result + (resultAbove - result) * (tempCond - temp) - tempSubCol;
                }
            }
            throw new TempToPresException(Calculation.TO.Main.Properties.Resources.ErrorSubColTemp);
        }
        #endregion
    }
}
