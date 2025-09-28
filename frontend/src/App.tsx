
import './App.css'
import Login from './pages/Login'
import ProductList from './pages/ProductList'
import { BrowserRouter, Route, Routes } from 'react-router-dom'
import ProductDetail from './pages/ProductDetail'
import AdminPage from './pages/AdminPage'
import CustomerRegister from './pages/CustomerRegister'

function App() {

  return (
    <BrowserRouter>
      <Routes>
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<CustomerRegister />} />
        <Route path="/" element={<ProductList />} />
        <Route path="/admin" element={<AdminPage />} />
        <Route path="/product/:id" element={<ProductDetail />} />
      </Routes>
    </BrowserRouter>
  )
}

export default App
