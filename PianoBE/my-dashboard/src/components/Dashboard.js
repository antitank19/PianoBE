import React, { useState, useEffect } from "react";
import { Card, CardContent } from "./ui/card";
import { BarChart, Bar, XAxis, YAxis, Tooltip, ResponsiveContainer } from "recharts";
import "./Dashboard.css";  // Import CSS mới

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
      .catch((error) => {
        console.error("Lỗi khi gọi API:", error);
        setLoading(false);
      });
  }, []);

  if (loading) {
    return <p className="text-center text-lg">Đang tải dữ liệu...</p>;
  }

  if (!data) {
    return <p className="text-center text-lg text-red-500">Lỗi tải dữ liệu từ API!</p>;
  }

  const chartData = data.playsInYear.map((item) => ({
    name: `Tháng ${item.month}`,
    value: item.numberPlays,
  }));

  return (
    <div id="dashboard-container">
      {/* Grid chứa các số liệu thống kê */}
      <div className="grid grid-cols-3 gap-6 mb-6">
        <Card className="stat-card">
          <CardContent>
            <h2 className="stat-title">Số người dùng</h2>
            <p className="stat-value stat-users">{data.userNumber}</p>
          </CardContent>
        </Card>

        <Card className="stat-card">
          <CardContent>
            <h2 className="stat-title">Số người đang hoạt động</h2>
            <p className="stat-value stat-active-users">{data.activeUserNumber}</p>
          </CardContent>
        </Card>

        <Card className="stat-card">
          <CardContent>
            <h2 className="stat-title">Số bài hát</h2>
            <p className="stat-value stat-songs">{data.numberSong}</p>
          </CardContent>
        </Card>
      </div>

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
