using Veza.HeatExchanger.Exceptions;
using System;
using System.Collections.Generic;
using Veza.HeatExchanger.Interfaces.Refrigerants;

namespace Veza.HeatExchanger.Services.Refrigerants.R513A
{
    /// <summary>
    /// Класс для перевода температуры в давление для хладогента R513A
    /// Данные взяты из программы Select 8
    /// </summary>
    sealed internal class RefrigerantR513A : IRefrigerant
    {
        int max = 71;
        int min = -66;
        Dictionary<int, double> tempToPres = new Dictionary<int, double>()
        {
            {-65, 0.14},
            {-64, 0.15},
            {-63, 0.16},
            {-62, 0.17},
            {-61, 0.18},
            {-60, 0.19},
            {-59, 0.21},
            {-58, 0.22},
            {-57, 0.23},
            {-56, 0.25},
            {-55, 0.26},
            {-54, 0.28},
            {-53, 0.29},
            {-52, 0.31},
            {-51, 0.33},
            {-50, 0.35},
            {-49, 0.37},
            {-48, 0.39},
            {-47, 0.41},
            {-46, 0.44},
            {-45, 0.46},
            {-44, 0.49},
            {-43, 0.51},
            {-42, 0.54},
            {-41, 0.57},
            {-40, 0.6},
            {-39, 0.63},
            {-38, 0.66},
            {-37, 0.69},
            {-36, 0.73},
            {-35, 0.76},
            {-34, 0.8},
            {-33, 0.84},
            {-32, 0.88},
            {-31, 0.92},
            {-30, 0.97},
            {-29, 1.01},
            {-28, 1.06},
            {-27, 1.11},
            {-26, 1.16},
            {-25, 1.21},
            {-24, 1.26},
            {-23, 1.32},
            {-22, 1.38},
            {-21, 1.43},
            {-20, 1.5},
            {-19, 1.56},
            {-18, 1.62},
            {-17, 1.69},
            {-16, 1.76},
            {-15, 1.83},
            {-14, 1.91},
            {-13, 1.98},
            {-12, 2.06},
            {-11, 2.14},
            {-10, 2.23},
            {-9, 2.31},
            {-8, 2.4},
            {-7, 2.49},
            {-6, 2.59},
            {-5, 2.68},
            {-4, 2.78},
            {-3, 2.88},
            {-2, 2.99},
            {-1, 3.1},
            { 0, 3.21},
            { 1, 3.32},
            { 2, 3.44},
            { 3, 3.56},
            { 4, 3.68},
            { 5, 3.81},
            { 6, 3.94},
            { 7, 4.07},
            { 8, 4.21},
            { 9, 4.34},
            { 10, 4.49},
            { 11, 4.63},
            { 12, 4.78},
            { 13, 4.94},
            { 14, 5.1},
            { 15, 5.26},
            { 16, 5.42},
            { 17, 5.59},
            { 18, 5.77},
            { 19, 5.94},
            { 20, 6.12},
            {21, 6.31},
            {22, 6.5},
            {23, 6.69},
            {24, 6.89},
            {25, 7.09},
            {26, 7.30},
            {27, 7.51},
            {28, 7.73},
            {29, 7.95},
            {30, 8.17},
            {31, 8.40},
            {32, 8.64},
            {33, 8.88},
            {34, 9.12},
            {35, 9.37},
            {36, 9.63},
            {37, 9.89},
            {38, 10.16},
            {39, 10.43},
            {40, 10.7},
            {41, 10.98},
            {42, 11.27},
            {43, 11.57},
            {44, 11.86},
            {45, 12.17},
            {46, 12.48},
            {47, 12.8},
            {48, 13.12},
            {49, 13.45},
            {50, 13.78},
            {51, 14.12},
            {52, 14.47},
            {53, 14.82},
            {54, 15.18},
            {55, 15.55},
            {56, 15.92},
            {57, 16.30},
            {58, 16.69},
            {59, 17.08},
            {60, 17.48},
            {61, 17.89},
            {62, 18.30},
            {63, 18.72},
            {64, 19.15},
            {65, 19.58},
            {66, 20.03},
            {67, 20.48},
            {68, 20.94},
            {69, 21.40},
            {70, 21.88},
        };
        Dictionary<double, int> presToTemp = new Dictionary<double, int>()
        {
            {0.14, -65},
            {0.15, -64},
            {0.16, -63},
            {0.17, -62},
            {0.18, -61},
            {0.19, -60},
            {0.21, -59},
            {0.22, -58},
            {0.23, -57},
            {0.25, -56},
            {0.26, -55},
            {0.28, -54},
            {0.29, -53},
            {0.31, -52},
            {0.33, -51},
            {0.35, -50},
            {0.37, -49},
            {0.39, -48},
            {0.41, -47},
            {0.44, -46},
            {0.46, -45},
            {0.49, -44},
            {0.51, -43},
            {0.54, -42},
            {0.57, -41},
            {0.6, -40},
            {0.63, -39},
            {0.66, -38},
            {0.69, -37},
            {0.73, -36},
            {0.76, -35},
            {0.8, -34},
            {0.84, -33},
            {0.88, -32},
            {0.92, -31},
            {0.97, -30},
            {1.01, -29},
            {1.06, -28},
            {1.11, -27},
            {1.16, -26},
            {1.21, -25},
            {1.26, -24},
            {1.32, -23},
            {1.38, -22},
            {1.43, -21},
            {1.5, -20},
            {1.56, -19},
            {1.62, -18},
            {1.69, -17},
            {1.76, -16},
            {1.83, -15},
            {1.91, -14},
            {1.98, -13},
            {2.06, -12},
            {2.14, -11},
            {2.23, -10},
            {2.31, -9},
            {2.4, -8},
            {2.49, -7},
            {2.59, -6},
            {2.68, -5},
            {2.78, -4},
            {2.88, -3},
            {2.99, -2},
            {3.1, -1},
            {3.21, 0},
            {3.32, 1},
            {3.44, 2},
            {3.56, 3},
            {3.68, 4},
            {3.81, 5},
            {3.94, 6},
            {4.07, 7},
            {4.21, 8},
            {4.34, 9},
            {4.49, 10},
            {4.63, 11},
            {4.78, 12},
            {4.94, 13},
            {5.1, 14},
            {5.26, 15},
            {5.42, 16},
            {5.59, 17},
            {5.77, 18},
            {5.94, 19},
            {6.12, 20},
            {6.31, 21},
            {6.5, 22},
            {6.69, 23},
            {6.89, 24},
            {7.09, 25},
            {7.30, 26},
            {7.51, 27},
            {7.73, 28},
            {7.95, 29},
            {8.17, 30},
            {8.40, 31},
            {8.64, 32},
            {8.88, 33},
            {9.12, 34},
            {9.37, 35},
            {9.63, 36},
            {9.89, 37},
            {10.16, 38},
            {10.43, 39},
            {10.7, 40},
            {10.98, 41},
            {11.27, 42},
            {11.57, 43},
            {11.86, 44},
            {12.17, 45},
            {12.48, 46},
            {12.8, 47},
            {13.12, 48},
            {13.45, 49},
            {13.78, 50},
            {14.12, 51},
            {14.47, 52},
            {14.82, 53},
            {15.18, 54},
            {15.55, 55},
            {15.92, 56},
            {16.30, 57},
            {16.69, 58},
            {17.08, 59},
            {17.48, 60},
            {17.89, 61},
            {18.30, 62},
            {18.72, 63},
            {19.15, 64},
            {19.58, 65},
            {20.03, 66},
            {20.48, 67},
            {20.94, 68},
            {21.40, 69},
            {21.88, 70},
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
