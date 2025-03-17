import React, { useState, useEffect } from "react";
import { Card, CardContent } from "./ui/card";
import { BarChart, Bar, XAxis, YAxis, Tooltip, ResponsiveContainer } from "recharts";
import "./Dashboard.css";  // Import CSS mới
import Papa from "papaparse";

const getMonthlyTransactionsFromCSV = async () => {
  return new Promise((resolve, reject) => {
    Papa.parse("/Transaction_report_17_03_2025.csv", {
      download: true,
      header: true,
      skipEmptyLines: true,
      complete: (result) => {
        if (!result.data || result.data.length === 0) {
          reject("CSV parsing failed or file is empty");
          return;
        }

        const transactions = Array.from({ length: 12 }, (_, i) => ({
          month: i + 1,
          numberPlays: 0, // Default to 0 for missing months
        }));

        result.data.forEach((row, index) => {
          if (!row["Thời gian"] || !row["Số tiền"]) {
            console.warn(`⚠️ Missing data in row ${index + 1}:`, row);
            return; // Skip invalid rows
          }

          const dateParts = row["Thời gian"].split(" ")[0].split("-");
          if (dateParts.length < 2) {
            console.warn(`⚠️ Invalid date format in row ${index + 1}:`, row["Thời gian"]);
            return;
          }

          const monthKey = parseInt(dateParts[1], 10);
          const amount = parseFloat(row["Số tiền"]) || 0;
          
          transactions[monthKey - 1].numberPlays += amount; // Fill existing month
        });

        resolve(transactions);
      },
      error: (error) => reject(error),
    });
  });
};

const MOCK_PLAY = Array.from({ length: 12 }, (_, i) => ({
  month: i + 1,
  numberPlays: 0, 
}));

MOCK_PLAY[4].numberPlays = 5;

const MOCK_MONEY = await getMonthlyTransactionsFromCSV();

const Dashboard = () => {
  const [data, setData] = useState(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    fetch("http://localhost:7133/api/DashBoard?year=2025&dateStart=2025-01-1&dateEnd=2025-03-17")
      .then((response) => response.json())
      .then((json) => {
        setData(json);
        setLoading(false);
      })
      .catch(async (error) => {
        console.error("Lỗi khi gọi API, dùng dữ liệu CSV làm mock:", error);
        try {
          setData({
            playsInYear: MOCK_PLAY,
            userNumber: 200,
            activeUserNumber: 10,
            numberSong: 80,
            topSong: [
              { top: 1, songName: "Shape of You", numberPlays: 1500 },
              { top: 2, songName: "Blinding Lights", numberPlays: 1300 },
              { top: 3, songName: "Someone Like You", numberPlays: 1200 },
              { top: 4, songName: "Uptown Funk", numberPlays: 1100 },
              { top: 5, songName: "Bohemian Rhapsody", numberPlays: 1000 },
            ],
          });
        } catch (csvError) {
          console.error("Lỗi khi tải CSV:", csvError);
        }
        setLoading(false);
      });
  }, []);

  if (loading) {
    return <p className="text-center text-lg">Đang tải dữ liệu...</p>;
  }

  const chartData = data.playsInYear.map((item) => ({
    name: `Tháng ${item.month}`,
    value: item.numberPlays,
  }));

  const moneyData = MOCK_MONEY.map((item) => ({
    name: `Tháng ${item.month}`,
    value: item.numberPlays,
  }));


  return (
    <div id="dashboard-container">
      {/* Grid chứa các số liệu thống kê */}
      <div className="dashboard-layout">
        <Card>
          <CardContent>
            <h2 className="stat-title">Số người dùng</h2>
            <p className="stat-value stat-users">{data.userNumber}</p>
          </CardContent>
        </Card>

        <Card>
          <CardContent>
            <h2 className="stat-title">Số người đang hoạt động</h2>
            <p className="stat-value stat-active-users">{data.activeUserNumber}</p>
          </CardContent>
        </Card>

        <Card>
          <CardContent>
            <h2 className="stat-title">Số bài hát</h2>
            <p className="stat-value stat-songs">{data.numberSong}</p>
          </CardContent>
        </Card>
      </div>

      {/* Doanh thu */}
      <Card className="chart-container mb-6">
        <h2 className="text-xl font-bold mb-4">Doanh thu theo tháng</h2>
        <ResponsiveContainer width="100%" height={300}>
          <BarChart data={moneyData}>
            <XAxis dataKey="name" />
            <YAxis />
            <Tooltip />
            <Bar dataKey="value" fill="#82ca9d" />
          </BarChart>
        </ResponsiveContainer>
      </Card>

      {/* Biểu đồ */}
      <Card className="chart-container mb-6">
        <h2 className="text-xl font-bold mb-4">Lượt chơi theo tháng</h2>
        <ResponsiveContainer width="100%" height={300}>
          <BarChart data={chartData}>
            <XAxis dataKey="name" />
            <YAxis />
            <Tooltip />
            <Bar dataKey="value" fill="#82ca9d" />
          </BarChart>
        </ResponsiveContainer>
      </Card>

      {/* Danh sách bài hát */}
      <Card className="chart-container">
        <h2 className="text-xl font-bold mb-4">Top bài hát được chơi nhiều nhất</h2>
        <ul className="song-list">
          {data.topSong.map((song, index) => (
            <li key={index} className="song-item">
              #{song.top} {song.songName} - {song.numberPlays} lượt chơi
            </li>
          ))}
        </ul>
      </Card>
    </div>
  );
};

export default Dashboard;
