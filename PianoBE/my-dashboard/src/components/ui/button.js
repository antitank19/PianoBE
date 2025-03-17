import React from "react";

export const Button = ({ children, onClick }) => (
  <button
    className="bg-blue-500 text-white px-4 py-2 rounded-md hover:bg-blue-600 transition"
    onClick={onClick}
  >
    {children}
  </button>
);
