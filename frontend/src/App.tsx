
import './App.css'
import Login from './pages/Login'
import ProductList from './pages/ProductList'
import { BrowserRouter, Route, Routes } from 'react-router-dom'
import ProductDetail from './pages/ProductDetail'
import AdminPage from './pages/AdminPage'
import CustomerRegister from './pages/CustomerRegister'
import Error403 from './pages/Error403'
import Error401 from './pages/Error401'

function App() {

  return (
    <BrowserRouter>
      <Routes>
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<CustomerRegister />} />
        <Route path="/" element={<ProductList />} />
        <Route path="/admin" element={<AdminPage />} />
        <Route path="/product/:id" element={<ProductDetail />} />
        <Route path="*" element={<div className="p-8 text-center">Página não encontrada</div>} />
        <Route path="/403" element={<Error403 />} />
        <Route path="/401" element={<Error401 />} />
      </Routes>
    </BrowserRouter>
  )
}

export default App
