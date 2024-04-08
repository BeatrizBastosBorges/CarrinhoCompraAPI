# Carrinho de Compras
<br>
Este repositório contém um projeto de um carrinho de compras desenvolvido em C# ASP.NET Core 5.0.
<br>

## Funcionalidades
<br>
<ol>
  <li>Criar Produto</li>
  <ul>
    <li>Permite criar um novo produto para ser adicionado ao carrinho de compras.</li>
  </ul>
  <br>
  
  <li>Adicionar Produtos ao Carrinho</li>
  <ul>
    <li>Permite adicionar produtos previamente criados ao carrinho de compras.</li>
  </ul>
  <br>
  
  <li>Criar Compra</li>
  <ul>
    <li>Permite criar uma compra com um determinado número de parcelas desde que o valor da parcela não seja menor que R$ 40,00.</li>
    <li>Ao criar uma compra, associa os produtos selecionados à compra, calcula o quanto cada produto valerá na parcela e remove esses produtos do carrinho.</li>
  </ul>
  <br>
  
  <li>Abater Valor da Compra</li>
  <ul>
    <li>Permite abater um valor específico da compra.</li>
    <li>Recalcula o valor das parcelas da compra com o desconto aplicado.</li>
    <li>Recalcula o valor das parcelas dos produtos com o desconto aplicado.</li>
  </ul>
  <br>
</ol>

## Como Usar
<br>
<ol>
  <li>Clone este repositório:</li>
  <ul>
    <li>git clone https://github.com/BeatrizBastosBorges/CarrinhoCompraAPI.git</li>
  </ul>
  <br>

  <li>Instale as dependências:</li>
  <ul>
    <li>npm install</li>
  </ul>
  <br>

  <li>npm install</li>
  <ul>
    <li>npm start</li>
  </ul>
  <br>
</ol>
